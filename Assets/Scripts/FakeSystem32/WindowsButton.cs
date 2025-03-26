using UnityEngine;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class WindowsButton : MonoBehaviour
{
    public Canvas canvas;
    [Tooltip("If this WindowsButton is on a prefab it needs to be set in code if not then set it here and it won't size it wrong")]
    public GridLayoutGroup layoutGroup;
    public Desktop desktop;
    public Additive_Scene_Handler additiveSceneHandler;
    public SpriteHandlerScript spriteHandlerScript;
    [Tooltip("We'll use this to spawn the application we wanna open, everything else will be done by the application we open (like putting icons in the taskbar)")]
    public GameObject applicationToOpen;
    [Tooltip("This doesn't get set in editor")]
    public GameObject application;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();            
            desktop = GameObject.Find("DesktopUI").GetComponent<Desktop>();
            if (layoutGroup == null)
            {
                layoutGroup = GameObject.Find("DesktopUI").GetComponent<GridLayoutGroup>();
            }
            GetComponent<RectTransform>().sizeDelta = layoutGroup.cellSize;
            GetComponent<BoxCollider>().size = layoutGroup.cellSize;
            if (TryGetComponent<Additive_Scene_Handler>(out Additive_Scene_Handler sceneHandler))
            {
                additiveSceneHandler = sceneHandler;
                additiveSceneHandler.canvas = canvas;
                additiveSceneHandler.manager = GameObject.Find("ManagerOBJ").GetComponent<WindowSpawner>();
            }
        }
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
    public void SetUpVariables(FileData caller, GameObject application, int sceneIndex, Material cameraMaterial)
    {
        applicationToOpen = application;
        additiveSceneHandler.SetVariablesFromFileData(caller);
    }
    public void DropOntoGrid()
    {
        //find which empty space the icon is above then make it the child of it
        GameObject smallestDistanceObj = null;
        float smallestDistance = 10000000;
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
                }
            }
        }

        gameObject.transform.SetParent(smallestDistanceObj.transform);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    public void MoveToTopOfHierarchy()
    {
        gameObject.transform.SetParent(canvas.transform, true);
    }

    public void TestDouble()
    {
        print("Double Click!!");
    }
    public void OpenApplication()
    {
        application = Instantiate(applicationToOpen, canvas.gameObject.transform);
    }
    public void OpenApplicationAndSendCaller(FileData caller)
    {
        application = Instantiate(applicationToOpen, canvas.gameObject.transform);
        application.SendMessage("ReceiveCaller", caller);
    }
    public void ManagerSpawnWindow()
    {
        additiveSceneHandler.manager.SpawnWindow();
    }
}
