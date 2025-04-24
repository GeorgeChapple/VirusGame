using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
*/
public class WindowsButton : MonoBehaviour
{
    [Header("Manual Input Settings")]
    public Canvas canvas;
    public Camera cam;
    public GameObject hierarchy;
    [Tooltip("If this WindowsButton is on a prefab it needs to be set in code if not then set it here and it won't size it wrong")]
    public GridLayoutGroup layoutGroup;
    public AdditiveSceneHandler additiveSceneHandler;
    public SpriteHandlerScript spriteHandlerScript;
    [Tooltip("We'll use this to spawn the application we wanna open, everything else will be done by the application we open (like putting icons in the taskbar)")]
    public GameObject applicationToOpen;
    public LayerMask dropLayer;
    public LayerMask iconLayer;
    public GameObject fileExplorerIconPrefab;
    public bool isFileExplorerIcon;
    public FileData file;
    public FileData fileThatDroppedOnUs;
    public UnityEvent DropOnIconEvent;

    public enum IconState { Desktop = 0, Taskbar = 1, FileExplorer = 2 };
    public IconState iconState = IconState.Desktop;

    [Header("Non-Manual Input Settings")]
    public bool canBeTaskbarIcon;
    [HideInInspector] public GameObject application;
    public Desktop desktop;
    public Taskbar taskbar;
    public GameObject previousParent;
    public int previousParentChildPos;
    private FileExplorer fileExplorer;
    public bool canBeDragged;
    private void Awake()
    {
        hierarchy = GameObject.Find("WindowHierarchy");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (canvas == null)
        {
            canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();
            desktop = GameObject.Find("DesktopUI").GetComponent<Desktop>();
            taskbar = GameObject.Find("TaskBar").GetComponentInChildren<Taskbar>();
            if (layoutGroup == null)
            {
                layoutGroup = GameObject.Find("DesktopUI").GetComponent<GridLayoutGroup>();
            }
            GetComponent<RectTransform>().sizeDelta = layoutGroup.cellSize;
            GetComponent<BoxCollider>().size = layoutGroup.cellSize;
            if (TryGetComponent<AdditiveSceneHandler>(out AdditiveSceneHandler sceneHandler))
            {
                additiveSceneHandler = sceneHandler;
                additiveSceneHandler.canvas = canvas;
                additiveSceneHandler.manager = GameObject.Find("ManagerOBJ").GetComponent<WindowSpawner>();
            }
        }
    }
    private void Start()
    {
        if (!canBeDragged)
        {
            Destroy(GetComponent<WindowScript>());
        }
        application = applicationToOpen;
        
    }
    public void SetUpVariables(FileData caller, GameObject application)
    {
        applicationToOpen = application;
    }
    public void SetUpVariables(FileData caller, GameObject application, SpriteHandlerScript spriteHandlerScriptPass)
    {
        applicationToOpen = application;
        spriteHandlerScript = spriteHandlerScriptPass;
        spriteHandlerScript.ReceiveSprites(caller);
        spriteHandlerScript.SetUp();
    }
    public void SetUpVariables(FileData caller, GameObject application, string sceneName, Material cameraMaterial)
    {
        applicationToOpen = application;
        additiveSceneHandler.SetVariablesFromFileData(caller);
    }
    public void DropOntoDesktopGrid(bool fromFileExplorer)
    {
        //check if its a file explorer icon first
        //find which empty space the icon is above then make it the child of it
        GameObject smallestDistanceObj = null;
        float smallestDistance = 10000000;
        int i = 0;
        int spotOnDesktop = 0;
        foreach (GameObject space in desktop.desktopSpaces)
        {
            //get distances to all
            float distance = Vector3.Distance(gameObject.transform.position, space.transform.position);
            //check if there is something in that space already
            if (space.transform.childCount <= 0)
            {
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    smallestDistanceObj = space;
                    spotOnDesktop = i;
                }
            }
            i++;
        }
        if (!fromFileExplorer)
        {
            gameObject.transform.SetParent(smallestDistanceObj.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            GetComponent<BoxCollider>().size = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y, 1);
            GetComponent<RectTransform>().sizeDelta = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y, 1);
            gameObject.transform.position = smallestDistanceObj.transform.position + new Vector3(0, 0, -1);
            TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            Color transparentCol = new Color(text.color.r, text.color.g, text.color.b, 255);
            text.color = transparentCol;
            iconState = IconState.Desktop;
        }
        else
        {
            desktop.SetUpIcon(file, spotOnDesktop);
            Destroy(gameObject);
        }
    }
    public void DropOntoTaskBarGrid(bool fromFileExplorer)
    {
        //check if its a file explorer icon first
        if (!fromFileExplorer)
        {
            if (canBeTaskbarIcon)
            {
                if (iconState == IconState.Taskbar)
                {
                    DropOnPreviousParent();
                }
                else
                {
                    gameObject.transform.SetParent(taskbar.gameObject.transform);
                    gameObject.transform.position = taskbar.transform.position;
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    gameObject.GetComponent<BoxCollider>().size = new Vector3(taskbar.grid.cellSize.x, taskbar.grid.cellSize.y, 1);
                    gameObject.transform.position = taskbar.transform.position + new Vector3(0, 0, -1);
                    TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                    Color transparentCol = new Color(text.color.r, text.color.g, text.color.b, 0);
                    text.color = transparentCol;
                    iconState = IconState.Taskbar;
                }

            }
            else
            {   //if can't the move it back
                DropOnPreviousParent();
            }
        }
        else
        {
            taskbar.SetUpTaskbarIcon(file);
            Destroy(gameObject);
        }

    }
    public void DropOntoFileExplorerGrid(bool fromFileExplorer)
    {
        if (!fromFileExplorer)
        {
            fileExplorer.SetUpButton(file, 0);
            Destroy(gameObject);
        }
        else
        {
            DropOnPreviousParent();
        }
    }
    public void DropOnPreviousParent()
    {
        Debug.Log("Drop on previous parent");
        gameObject.transform.SetParent(previousParent.transform);
        gameObject.transform.SetSiblingIndex(previousParentChildPos);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        //GetComponent<BoxCollider>().size = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y, 1);
        //GetComponent<RectTransform>().sizeDelta = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y, 1);
        //gameObject.transform.position = previousParent.transform.position + new Vector3(0, 0, -1);
    }
    public void HoverDrop()
    {
        if (!canBeDragged) return;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, Mathf.Infinity, iconLayer))
        { //if we dropped the icon onto another icon
            Debug.Log("dropped onto icon");
            hitInfo.transform.GetComponent<WindowsButton>().fileThatDroppedOnUs = file;
            hitInfo.transform.TryGetComponent<VirusScannerScript>(out VirusScannerScript virusScanner);
            if (virusScanner != null) { virusScanner.fileToScan = file; }            
            hitInfo.transform.GetComponent<WindowsButton>().DropOnIconEvent.Invoke();
            //tell the icon we dropped onto to do something with the icon we dropped it onto
            DropOnPreviousParent();
        }
        else if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, Mathf.Infinity, dropLayer))
        { //check if we dropped onto the desktop, taskbar or file explorer            
            if (hitInfo.transform.TryGetComponent(out Desktop desktop))
            {
                DropOntoDesktopGrid(isFileExplorerIcon);
            }
            else if (hitInfo.transform.TryGetComponent(out Taskbar taskbar))
            {
                DropOntoTaskBarGrid(isFileExplorerIcon);
            }
            else if (hitInfo.transform.parent.parent.TryGetComponent(out FileExplorer fileExplorerInstance))
            {
                fileExplorer = fileExplorerInstance;
                DropOntoFileExplorerGrid(isFileExplorerIcon);
            }
            else //if whatever we dropped on had the layer but none of these scripts
            {
                Debug.LogWarning("Dropped onto something with the correct layer but not something we can do anything with!");
                DropOnPreviousParent();
            }
        }
        else // if we somehow dropped onto nothing
        {
            Debug.LogWarning("Dropped onto nothing!");
            DropOnPreviousParent();
        }
    }
    public void SavePreviousPosition()
    {
        previousParent = gameObject.transform.parent.gameObject;
        previousParentChildPos = gameObject.transform.GetSiblingIndex();
    }
    public void MoveToTopOfHierarchy()
    {
        if (canBeDragged)
        {
            gameObject.transform.SetParent(canvas.transform, true);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
        }

    }
    public void CheckApplicationIsOpen()
    {
        GameObject go = GameObject.Find(applicationToOpen.name);
        if (go != null)
        {
            Debug.Log("close");
            CloseApplication(application);
        }
        else
        {
            Debug.Log("open");
            OpenApplication();
        }
    }
    public void OpenApplication()
    {
        application = Instantiate(applicationToOpen, hierarchy.transform);
        application.name = applicationToOpen.name;
    }
    public void OpenApplicationAndSendCaller(FileData caller)
    {
        application = Instantiate(applicationToOpen, hierarchy.transform);
        application.SendMessage("ReceiveCaller", caller);
        application.name = applicationToOpen.name;
    }
    public void CloseApplication(GameObject appToClose)
    {
        Destroy(appToClose);
    }
    public void ManagerSpawnWindow()
    {
        additiveSceneHandler.manager.SpawnWindow();
    }
}