using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class FileExplorer : MonoBehaviour
{
    //list all files and make the buttons for them

    [SerializeField] private GameObject sideBar;
    [SerializeField] private GameObject pathBar;
    public GameObject contentArea;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private FileData currentFolder;
    public string currentPath;

    private void Awake()
    {
        SetUpUI();
    }
    public void ChangeCurrentFolderDesktop(FileData file)
    {
        currentFolder = file;
        SetUpUI();
    }
    public void ChangeCurrentFolder(int num)
    {
        currentFolder = currentFolder.children[num];
        SetUpUI();
    }

    public void SetUpUI()
    {
        //delete all buttons that exist right now and reset button area
        for (int i = 0; i < contentArea.transform.childCount; i++)
        {
            Destroy(contentArea.transform.GetChild(i).gameObject);
        }
        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, 80);

        //spawn new buttons
        foreach (var (file, i) in currentFolder.children.Select((value, i) => (value, i)))
        {
            if (file == null || !file.isAvailable)
            {
                continue;
            }
            GameObject button = Instantiate(buttonPrefab, contentArea.transform); //spawn button then set some variables

            button.GetComponent<WindowsButton>().layoutGroup = contentArea.GetComponent<GridLayoutGroup>();
            button.GetComponent<BoxCollider>().size = button.GetComponent<WindowsButton>().layoutGroup.cellSize;             //add space for new folders
            contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, contentArea.GetComponent<RectTransform>().sizeDelta.y + 80);
            button.GetComponentInChildren<TextMeshProUGUI>().text = file.name;
            button.GetComponentInChildren<Image>().sprite = file.icon[0];

            button.GetComponent<WindowsButton>().SetUpVariables(file, file.application, button.GetComponent<SpriteHandlerScript>());
            button.GetComponent<WindowsButton>().SetUpVariables(file, file.application, file.sceneName, file.cameraMaterial);


            if (file.dataType == FileData.DataType.Folder)
            {
                button.GetComponent<HitEventScript>().doubleHitEvent.RemoveAllListeners();
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(delegate { ChangeCurrentFolder(i); });
                continue;
            }
            else if (file.dataType == FileData.DataType.Application)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = file.name + ".exe";
                button.GetComponent<WindowsButton>().applicationToOpen = file.application;                        
            }
            else if (file.dataType == FileData.DataType.Image)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = file.name + ".png";
            }
            foreach (EventPass eventPass in file.OnDoubleClick)
            {
                UnityAction action;
                string methodName = eventPass.methodName; //get event values out
                int intVal = eventPass.intVal;
                float floatVal = eventPass.floatVal;
                string stringVal = eventPass.stringVal;
                FileData selfVal = eventPass.self;
                if (eventPass.passValThrough)
                {
                    if (eventPass.passIntVal)
                    {
                        action = new UnityAction(delegate { button.SendMessage(methodName, intVal); }); //create new event which calls the methodname on every monobehaviour obj
                    }
                    else if (eventPass.passFloatVal)
                    {
                        action = new UnityAction(delegate { button.SendMessage(methodName, floatVal); });
                    }
                    else if (eventPass.passStringVal)
                    {
                        action = new UnityAction(delegate { button.SendMessage(methodName, stringVal); });
                    }
                    else if (eventPass.passSelfVal)
                    {
                        action = new UnityAction(delegate { button.SendMessage(methodName, selfVal); });
                    }
                    else
                    {
                        action = new UnityAction(delegate { button.SendMessage(methodName); });
                    }
                }
                else
                {
                    action = new UnityAction(delegate { button.SendMessage(methodName); });
                }
                if (action != null)
                {
                    button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action); //add it to the events
                }
            }
        }
        //write out the path to root backwards then reverse it and make it a string again
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
    public void ReceiveCaller(FileData file)
    {
        ChangeCurrentFolderDesktop(file);
    }
}
