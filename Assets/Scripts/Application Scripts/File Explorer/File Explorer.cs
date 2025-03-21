using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

                FileType fileType = new FileType(); //create new filetype object

                string[] splitLine = line.Split(new char[] { '-' }); //split lines into there respective types

                string filePointer = splitLine[0].Trim(); //set new strings for these types
                string name = splitLine[1].Trim(new char[] { ' ', '1', '2' });
                string dataType = splitLine[2].Trim(); 

                fileType.filePointer = Int32.Parse(filePointer); //put this data into new fileType object
                fileType.name = name;
                fileType.dataType = dataType;

                files.Add(fileType);//add to list of root

                nextLine += line;
                yield return null;
            }
        }
        yield return null;
    }
}

