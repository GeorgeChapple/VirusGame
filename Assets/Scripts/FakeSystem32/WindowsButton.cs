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
    [Tooltip("We'll use this to spawn the application we wanna open, everything else will be done by the application we open (like putting icons in the taskbar)")]
    public GameObject applicationToOpen;

    //public bool isPrefab = true;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();
            if (layoutGroup == null)
            {
                layoutGroup = GameObject.Find("Desktop").GetComponent<GridLayoutGroup>();
            }
            desktop = GameObject.Find("Desktop").GetComponent<Desktop>();
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

    public void DropOntoGrid()
    {
        //find which empty space the icon is above then make it the child of it
        //Dictionary<GameObject, float> distances = new Dictionary<GameObject, float>();
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
        //Debug.Log("smallest distance was " +  smallestDistance);

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
        Instantiate(applicationToOpen, canvas.gameObject.transform);
    }

    public void ManagerSpawnWindow()
    {
        additiveSceneHandler.manager.SpawnWindow();
    }
}
