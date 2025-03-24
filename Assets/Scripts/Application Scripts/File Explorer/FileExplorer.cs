using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileExplorer : MonoBehaviour
{
    //list all files and make the buttons for them

    [SerializeField] private GameObject sideBar;
    [SerializeField] private GameObject pathBar;
    [SerializeField] private GameObject contentArea;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private FileData currentFolder;
    public string currentPath;

    private void Awake()
    {
        //currentFolder = GameObject.Find("Directory").GetComponent<FileData>();
        SetUpUI();
    }

    public void ChangeCurrentFolder(int num)
    {
        currentFolder = currentFolder.children[num];
    }

    public void SetUpUI()
    {


        //delete all buttons that exist right now

        for (int i = 0; i < contentArea.transform.childCount; i++)
        {
            Destroy(contentArea.transform.GetChild(i).gameObject);
        }
        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, 80);

        //spawn new buttons
        foreach (var (file, i) in currentFolder.children.Select((value, i) => (value, i)))
        {

            GameObject button = Instantiate(buttonPrefab, contentArea.transform); //spawn button then set some variables

            FileDataObject fileData = button.AddComponent<FileDataObject>();
            fileData.root = file.root;
            fileData.icon = file.icon;
            fileData.name = file.name;
            fileData.dataType = file.dataType;
            fileData.children = file.children;
            fileData.parent = file.parent;
            fileData.fileExplorer = this;
            fileData.application = file.application;

            button.GetComponent<WindowsButton>().layoutGroup = contentArea.GetComponent<GridLayoutGroup>();
            button.GetComponent<BoxCollider>().size = button.GetComponent<WindowsButton>().layoutGroup.cellSize;             //add space for new folders
            contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, contentArea.GetComponent<RectTransform>().sizeDelta.y + 80);
            button.GetComponentInChildren<TextMeshProUGUI>().text = file.name;
            button.GetComponentInChildren<Image>().sprite = file.icon;

            

            if (file.dataType == "Folder")
            {
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(delegate { ChangeCurrentFolder(i); });
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(SetUpUI);
            }
            if (file.dataType == "Application")
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = file.name + ".exe";
                button.GetComponent<WindowsButton>().applicationToOpen = file.application;
                //button.GetComponent<HitEventScript>().doubleHitEvent.AddListener();
            }

        }
        currentPath = "";
        FileData fileName = currentFolder;
        while (!fileName.root)
        {
            currentPath += "/" + Reverse(fileName.name);
            fileName = fileName.parent;
        }
        currentPath += Reverse(fileName.name);
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
}
