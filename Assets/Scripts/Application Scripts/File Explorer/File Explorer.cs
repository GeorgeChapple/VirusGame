using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class FileExplorer : MonoBehaviour
{
    //so this will quite literally be a file explorer
    //now I would like to make it as proper as possible like having the desktop folder show what is in there
    //but since that would probably mean I'd have to remake
    //how the desktop icons work and stuff with making structs for types of data(applications, images, etc.)
    //ofc the only reason to do that would make it easier to add to and more modular
    //but we don't have a lot of time so i'm just gonna make it work continuing on with how ive been doing it

    [SerializeField] private GameObject sideBar;
    [SerializeField] private GameObject pathBar;

    [SerializeField] private TextAsset file;
    private string filePath;
    private string errorMessage = "***Syntax Error***";

    public List<FileType> files = new List<FileType>();

    public FileType rootFile;
    public FileType currentParent;
    private FileType currentFile;

    public GameObject prefab;

    void Awake()
    {
        filePath = AssetDatabase.GetAssetPath(file);
        StartRead();
    }
    public void StartRead()
    {
        StartCoroutine(ReadPathsAndSetUp());
        //ReadPathsAndSetUp();
    }
    private IEnumerator ReadPathsAndSetUp()
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            //for loop here
            //this needs to be instant

            //for loop each line
            //read from left to right
            //check for number at start(this will tell it if it has a parent)
            //anything after would be name
            //after look for ¬
            //after this is data type

            string line;
            string nextLine = "";
            while ((line = sr.ReadLine()) != null)
            {
                nextLine = "";

                //test
                GameObject obj = Instantiate(prefab, transform);
                obj.name = "p" + UnityEngine.Random.Range(0, 100);
                //FileType fileType = obj.AddComponent<FileType>();
                //FileType fileType = new FileType(); //create new filetype object
                currentFile = null;
                currentFile = obj.AddComponent<FileType>();

                if (currentParent == null)
                {
                    currentParent = currentFile;
                }

                string[] splitLine = line.Split(new char[] { '-' }); //split lines into their respective types

                //this is where we would check if we add this to become a child of another folder
                //maybe think about allocating some memory to making new variables in code(make new lists n stuff idk)
                //i have an idea for this but we'd have to see

                //check what the file pointer is then try to add it to the children of the previous one
                Int32.TryParse(splitLine[0].Trim(), out int fPointer);

                if (fPointer == 0)
                {
                    //its the root
                    rootFile = currentFile;
                    rootFile.SetUp();
                    currentParent = rootFile;
                }
                else if (fPointer == currentParent.filePointer + 1)
                {
                    //its the parents child
                    //Debug.Log("blah");
                    //currentParent.children.Append<FileType>(currentFile);

                    //add children
                    currentParent.AddToChildren(currentFile);

                }
                else if (fPointer < currentParent.filePointer)
                {
                    //next folder
                    //this could go from p5
                    //to p3
                    //so this needs to be modular

                    //use current file pointer to find the last folder of that pointer
                    //find current line index
                    //roll back until found current file pointer
                    //change current parent
                    //add to children of current parent

                    int temp = currentParent.filePointer;
                    while (files[temp].filePointer != fPointer)
                    {
                        temp--;
                    }
                    currentParent = files[temp].parent;
                    Debug.Log("aa");
                }
                else
                {
                    Debug.LogError("InvalidFilePointer: Next line's file pointer int needs to be + or - 1 of the previous's file pointer");
                    yield return null;
                }

                string filePointer = splitLine[0].Trim(); //set new strings for these types
                string name = splitLine[1].Trim(new char[] { ' ' });
                string dataType = splitLine[2].Trim();

                currentFile.filePointer = fPointer; //put this data into new fileType object
                currentFile.fileName = name;
                currentFile.dataType = dataType;
                currentFile.parent = currentParent;
                currentParent = currentFile;

                files.Add(currentFile);//add to list of root

                nextLine += line;
                yield return null;
            }
        }
        yield return null;
    }
}