using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject contentArea;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private FileType currentFolder;
    [SerializeField] private GameObject currentFolderObject;

    [SerializeField] private TextAsset file;
    private string filePath;

    public List<FileType> files = new List<FileType>();

    public FileType rootFile;
    public FileType currentParent;
    private FileType currentFile;
    public string currentPath;

    void Awake()
    {
        filePath = AssetDatabase.GetAssetPath(file);
        StartRead();
    }
    public void StartRead()
    {
        StartCoroutine(ReadPaths());
        //ReadPathsAndSetUp();
    }
    private IEnumerator ReadPaths()
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            string nextLine = "";
            while ((line = sr.ReadLine()) != null)
            {
                nextLine = "";
                currentFile = null;
                currentFile = new FileType();

                if (currentParent == null)
                {
                    currentParent = currentFile;
                }

                string[] splitLine = line.Split(new char[] { '-' }); //split lines into their respective types

                Int32.TryParse(splitLine[0].Trim(), out int fPointer); //get current file pointer
                string name = splitLine[1].Trim(new char[] { ' ' });//set new strings for these types
                string dataType = splitLine[2].Trim();

                currentFile.filePointer = fPointer; //put this data into new fileType object
                currentFile.fileName = name;
                currentFile.dataType = dataType;
                currentFile.fileExplorer = this;

                if (fPointer == 0)
                {
                    //its the root
                    rootFile = currentFile;
                    rootFile.root = true;
                    currentParent = rootFile;
                }
                else if (fPointer == currentParent.filePointer + 1)
                {
                    //its the parents child
                    //add children
                    currentParent.AddToChildren(currentFile);

                }
                else if (fPointer < currentParent.filePointer)
                {
                    //next folder
                    int temp = currentParent.filePointer;
                    while (files[temp].filePointer != fPointer)
                    {
                        temp--;
                    }
                    currentParent = files[temp].parent;
                    currentParent.AddToChildren(currentFile);
                }
                else if (fPointer == currentParent.filePointer)
                {
                    currentParent = currentParent.parent;
                    currentParent.AddToChildren(currentFile);
                }
                else
                {
                    Debug.LogError("InvalidFilePointer: Next line's file pointer int needs to be + or less than the previous's file pointer");
                    yield return null;
                }


                currentFile.parent = currentParent;
                currentParent = currentFile;

                files.Add(currentFile);//add to list of root

                nextLine += line; //go to next line
                yield return null;
            }
        }
        currentFolder = rootFile.children[0];
        SetUpUI();
        yield return null;
    }

    //start doing ui stuff
    private void SetUpUI()
    {
        //clear main content area

        for (int i = 0; i < contentArea.transform.childCount; i++)
        {
            Destroy(contentArea.transform.GetChild(i).gameObject);
        }
        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, 80);

        //set up functionality and folders in ui 
        foreach (var file in currentFolder.children)
        {
            GameObject button = Instantiate(buttonPrefab, contentArea.transform);
            currentFolderObject = button;
            button.GetComponent<WindowsButton>().layoutGroup = contentArea.GetComponent<GridLayoutGroup>();
            button.GetComponent<BoxCollider>().size = button.GetComponent<WindowsButton>().layoutGroup.cellSize;             //add space for new folders
            contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x , contentArea.GetComponent<RectTransform>().sizeDelta.y + 80);
            button.GetComponentInChildren<TextMeshProUGUI>().text = file.fileName;
            //button.GetComponentInChildren<Image>().sprite = file.icon;

            FileTypeObject fto = button.AddComponent<FileTypeObject>();
            fto.root = file.root;
            fto.icon = file.icon;
            fto.fileName = file.fileName;
            fto.dataType = file.dataType;
            fto.filePointer = file.filePointer;
            fto.children = file.children;
            fto.parent = file.parent;
            fto.fileExplorer = this;


            if (file.dataType == "Folder")
            {
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(button.GetComponent<FileTypeObject>().ReceiveFolder);
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(SetUpUI);
            }
            if (file.dataType == "Application")
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = file.fileName + ".exe";
                //button.GetComponent<WindowsButton>().applicationToOpen =
                //button.GetComponent<HitEventScript>().doubleHitEvent.AddListener();
            }

        }

        currentPath = "";
        FileType fileName = currentFolder;
        while (!fileName.root)
        {
            currentPath += "/" + Reverse(fileName.fileName);
            fileName = fileName.parent;
        }
        currentPath += Reverse(fileName.fileName);
        currentPath = Reverse(currentPath);
        pathBar.GetComponentInChildren<TextMeshProUGUI>().text = currentPath;
    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    public void GoBack()
    {
        currentFolder = currentFolder.parent;
        SetUpUI();
    }
    public void NewFolder()
    {

    }
    public void DeleteFolder()
    {

    }
    private void RewritePathFile()
    {

    }
    public void Retreive(FileTypeObject current)
    {
        currentFolder = currentFolder.Convert(current);
    }
}