using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : To handle everything an app icon should do, 
                        like being placed on the desktop or file explorer.
                        Think of FileData as the data and this script as the actual button logic.
*/
public class WindowsButton : MonoBehaviour
{
    [Header("Manual Input Settings")]
    public Canvas canvas;
    public Camera cam;
    public GameObject hierarchy;
    [Tooltip("Used for layout calculations when placed in a prefab.")]
    public GridLayoutGroup layoutGroup;
    public bool useLayoutGroup = true;
    public AdditiveSceneHandler additiveSceneHandler;
    public SpriteHandlerScript spriteHandlerScript;

    [Tooltip("Prefab of the app to open when the icon is clicked.")]
    public GameObject applicationToOpen;
    public LayerMask dropLayer;
    public LayerMask iconLayer;
    public GameObject fileExplorerIconPrefab;
    public bool isFileExplorerIcon;
    public FileData file;
    public FileData fileThatDroppedOnUs;
    public UnityEvent DropOnIconEvent;

    // Defines the icon's current state (for use with HoverDrop function)
    public enum IconState { Desktop = 0, Taskbar = 1, FileExplorer = 2 };
    public IconState iconState = IconState.Desktop;

    [Header("Non-Manual Input Settings")]
    public bool canBeTaskbarIcon;
    [HideInInspector] public GameObject application;
    public Desktop desktop;
    public Taskbar taskbar;
    public GameObject previousParent;
    public int previousParentChildPos;

    // Internal reference to the File Explorer (set dynamically)
    private FileExplorer fileExplorer;
    public bool canBeDragged;

    private void Awake()
    {
        // Assign required references if not already set
        hierarchy = GameObject.Find("WindowHierarchy");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (canvas == null)
        {
            canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();
            desktop = GameObject.Find("DesktopUI").GetComponent<Desktop>();
            taskbar = GameObject.Find("TaskBar").GetComponentInChildren<Taskbar>();

            // Set sizing using layout group if required
            if (useLayoutGroup)
            {
                if (layoutGroup == null)
                {
                    layoutGroup = GameObject.Find("DesktopUI").GetComponent<GridLayoutGroup>();
                }
                GetComponent<RectTransform>().sizeDelta = layoutGroup.cellSize;
                GetComponent<BoxCollider>().size = layoutGroup.cellSize;
            }

            // Automatically hook into AdditiveSceneHandler if present
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
        // Remove window script if dragging is not allowed
        if (!canBeDragged)
        {
            Destroy(GetComponent<WindowScript>());
        }
    }

    // Basic setup with file and app
    public void SetUpVariables(FileData caller, GameObject application)
    {
        applicationToOpen = application;
    }

    // Setup with sprite handler
    public void SetUpVariables(FileData caller, GameObject application, SpriteHandlerScript spriteHandlerScriptPass)
    {
        applicationToOpen = application;
        spriteHandlerScript = spriteHandlerScriptPass;
        spriteHandlerScript.ReceiveSprites(caller);
        spriteHandlerScript.SetUp();
    }

    // Setup for scene-based application
    public void SetUpVariables(FileData caller, GameObject application, string sceneName, Material cameraMaterial)
    {
        applicationToOpen = application;
        additiveSceneHandler.SetVariablesFromFileData(caller);
    }

    // Handles dropping an icon onto the desktop grid
    public void DropOntoDesktopGrid(bool fromFileExplorer, Vector3 posToCheckDist)
    {
        GameObject smallestDistanceObj = null;
        float smallestDistance = 10000000;
        int i = 0;
        int spotOnDesktop = 0;

        // Find the nearest empty desktop slot
        foreach (GameObject space in desktop.desktopSpaces)
        {
            float distance = Vector3.Distance(posToCheckDist, space.transform.position);
            if (space.transform.childCount <= 0 && distance < smallestDistance)
            {
                smallestDistance = distance;
                smallestDistanceObj = space;
                spotOnDesktop = i;
            }
            i++;
        }

        if (!fromFileExplorer)
        {
            // Place icon in the closest empty desktop slot
            transform.SetParent(smallestDistanceObj.transform);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            GetComponent<BoxCollider>().size = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y, 1);
            GetComponent<RectTransform>().sizeDelta = new Vector3(desktop.grid.cellSize.x, desktop.grid.cellSize.y);
            transform.position = smallestDistanceObj.transform.position + new Vector3(0, 0, -1);

            // Reset label transparency
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);

            iconState = IconState.Desktop;
        }
        else
        {
            // Spawn desktop icon if dropped from file explorer
            desktop.SetUpIcon(file, spotOnDesktop);
            Destroy(gameObject);
        }
    }

    // Handles dropping icon onto the taskbar
    public void DropOntoTaskBarGrid(bool fromFileExplorer)
    {
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
                    transform.SetParent(taskbar.transform);
                    transform.position = taskbar.transform.position;
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    GetComponent<BoxCollider>().size = new Vector3(taskbar.grid.cellSize.x, taskbar.grid.cellSize.y, 1);
                    transform.position += new Vector3(0, 0, -1);

                    // Hide the label
                    TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

                    iconState = IconState.Taskbar;
                }
            }
            else
            {
                // Return icon if it can't be added to taskbar
                DropOnPreviousParent();
            }
        }
        else
        {
            taskbar.SetUpTaskbarIcon(file);
            Destroy(gameObject);
        }
    }

    // Handles dropping icon into the file explorer
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

    // Restore icon to its original position
    public void DropOnPreviousParent()
    {
        transform.SetParent(previousParent.transform);
        transform.SetSiblingIndex(previousParentChildPos);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    // Handles icon drop events based on where it was dropped
    public void HoverDrop()
    {
        if (!canBeDragged) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] iconHits = Physics.RaycastAll(ray, Mathf.Infinity, iconLayer);
        RaycastHit[] dropHits = Physics.RaycastAll(ray, Mathf.Infinity, dropLayer);

        if (iconHits.Length > 1)
        {
            // Dropped onto another icon
            iconHits[1].transform.GetComponent<WindowsButton>().fileThatDroppedOnUs = file;
            iconHits[1].transform.TryGetComponent<VirusScannerScript>(out VirusScannerScript virusScanner);
            if (virusScanner != null) virusScanner.fileToScan = file;
            iconHits[1].transform.GetComponent<WindowsButton>().DropOnIconEvent.Invoke();
            DropOnPreviousParent();
        }
        else if (dropHits.Length > 0)
        {
            // Dropped onto a UI element
            if (dropHits[0].transform.TryGetComponent(out Desktop desktop))
            {
                DropOntoDesktopGrid(isFileExplorerIcon, dropHits[0].point);
            }
            else if (dropHits[0].transform.TryGetComponent(out Taskbar taskbar))
            {
                DropOntoTaskBarGrid(isFileExplorerIcon);
            }
            else if (dropHits[0].transform.parent.parent.TryGetComponent(out FileExplorer fileExplorerInstance))
            {
                fileExplorer = fileExplorerInstance;
                DropOntoFileExplorerGrid(isFileExplorerIcon);
            }
            else
            {
                Debug.LogWarning("Dropped onto something with the correct layer but not a valid drop target.");
                DropOnPreviousParent();
            }
        }
        else
        {
            Debug.LogWarning("Dropped onto nothing!");
            DropOnPreviousParent();
        }
    }

    // Saves the icon's current parent and sibling index
    public void SavePreviousPosition()
    {
        previousParent = transform.parent.gameObject;
        previousParentChildPos = transform.GetSiblingIndex();
    }

    // Move icon to front of UI hierarchy
    public void MoveToTopOfHierarchy()
    {
        if (canBeDragged)
        {
            transform.SetParent(canvas.transform, true);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    // Toggle app open/close based on current state
    public void CheckApplicationIsOpen()
    {
        GameObject go = GameObject.Find(applicationToOpen.name);
        if (go != null)
        {
            CloseApplication(application);
        }
        else
        {
            OpenApplication();
        }
    }

    // Open the assigned application
    public void OpenApplication()
    {
        if (application != null) return;
        application = Instantiate(applicationToOpen, hierarchy.transform);
        application.name = applicationToOpen.name;
    }

    // Open the app and send the FileData caller to it
    public void OpenApplicationAndSendCaller(FileData caller)
    {
        if (application != null) return;
        application = Instantiate(applicationToOpen, hierarchy.transform);
        application.SendMessage("ReceiveCaller", caller);
        application.name = applicationToOpen.name;
    }

    // Close the provided application
    public void CloseApplication(GameObject appToClose)
    {
        Destroy(appToClose);
    }

    // Open app via WindowSpawner manager
    public void ManagerSpawnWindow()
    {
        if (application != null) return;
        application = additiveSceneHandler.manager.SpawnWindow(applicationToOpen);
    }
}