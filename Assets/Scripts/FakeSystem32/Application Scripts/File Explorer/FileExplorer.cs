using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To mimic the real file explorer
                        on your desktop.
                        It essentially allows you to access all
                        of the fileData files in its own window.
*/
public class FileExplorer : MonoBehaviour
{

    [SerializeField] private GameObject sideBar; // Never ended up using this, never got round to it.
    [SerializeField] private GameObject pathBar;
    public GameObject contentArea;
    [SerializeField] private GameObject buttonPrefab;

    public FileData currentFolder;
    public string currentPath;

    private GameEventsManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<GameEventsManager>();
        SetUpUI(); // Set up buttons for user
    }
    // Sets the current folder and updates the UI, used externally
    public void ChangeCurrentFolderDesktop(FileData file)
    {
        currentFolder = file;
        SetUpUI();
    }
    // Sets the current folder and updates the UI
    public void ChangeCurrentFolder(int num)
    {
        currentFolder = currentFolder.children[num];
        SetUpUI();
    }
    // Creates and configures a new button in the UI for a given file
    public void SetUpButton(FileData file, int numOfFolder)
    {
        GameObject button = Instantiate(buttonPrefab, contentArea.transform); // Spawn button

        // Then set some variables
        WindowsButton wbComp = button.GetComponent<WindowsButton>();
        wbComp.layoutGroup = contentArea.GetComponent<GridLayoutGroup>();
        button.GetComponent<BoxCollider>().size = button.GetComponent<WindowsButton>().layoutGroup.cellSize;             // Add space for new folders
        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, contentArea.GetComponent<RectTransform>().sizeDelta.y + 80);

        // Set up visuals
        button.GetComponentInChildren<TextMeshProUGUI>().text = file.name;
        button.GetComponentInChildren<Image>().sprite = file.icon[0];

        // Pass file info to WindowsButton script
        wbComp.SetUpVariables(file, file.application, button.GetComponent<SpriteHandlerScript>());
        wbComp.SetUpVariables(file, file.application, file.sceneName, file.cameraMaterial);
        wbComp.canBeDragged = file.canBeDragged;
        wbComp.canBeTaskbarIcon = file.canBeTaskBarIcon;
        wbComp.iconState = WindowsButton.IconState.FileExplorer;
        wbComp.file = file;

        // Set dataType specific behavior
        if (file.dataType == FileData.DataType.Folder)
        {
            button.GetComponent<HitEventScript>().doubleHitEvent.RemoveAllListeners();
            button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(delegate { ChangeCurrentFolder(numOfFolder); });
            return;
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
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = file.name + ".file";
        }

        // Set up additional double-click events as defined in the file
        foreach (EventPass eventPass in file.OnDoubleClick)
        {
            UnityAction action;

            // Get event values out
            string methodName = eventPass.methodName;
            int intVal = eventPass.intVal;
            float floatVal = eventPass.floatVal;
            string stringVal = eventPass.stringVal;
            FileData selfVal = eventPass.self;

            if (eventPass.passValThrough)
            {
                // Depending on the value type, pass it to the method
                if (eventPass.passIntVal)
                {
                    action = new UnityAction(delegate { button.SendMessage(methodName, intVal); }); // Create new event which calls the methodname on every monobehaviour obj
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
            if (action != null) // Add it to the double click events
            {
                button.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action);
            }
        }
    }
    // Rebuilds the UI based on the current folder's contents
    public void SetUpUI()
    {
        // Delete all buttons that exist right now and reset button area
        for (int i = 0; i < contentArea.transform.childCount; i++)
        {
            Destroy(contentArea.transform.GetChild(i).gameObject);
        }
        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, 80);

        // Spawn new buttons
        foreach (var (file, i) in currentFolder.children.Select((value, i) => (value, i)))
        {

            if (file == null || !file.isAvailable)
            {
                continue;
            }
            if (currentFolder.name == "Desktop")
            {
                using (StreamReader sr = new StreamReader(manager.icons_SaveFilePath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains(file.name) && line[0] == '1')
                        {
                            SetUpButton(file, i);
                        }
                    }
                }
            }
            else
            {
                SetUpButton(file, i);
            }
            
        }

        // Write out the path to root backwards then reverse it and make it a string again
        currentPath = "";
        FileData fileName = currentFolder;
        while (!fileName.root)
        {
            currentPath += "/" + Reverse(fileName.name);
            fileName = fileName.parent;
        }
        currentPath += Reverse(fileName.name);
        currentPath = Reverse(currentPath);
        // Then show it to the user, so they know where they're at
        pathBar.GetComponentInChildren<TextMeshProUGUI>().text = currentPath;
    }

    // Reverses a string
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    // Go back to parent folder
    public void GoBack()
    {
        currentFolder = currentFolder.parent;
        SetUpUI();
    }
    // Called externally to change folder to a given file
    public void ReceiveCaller(FileData file)
    {
        ChangeCurrentFolderDesktop(file);
    }
}
