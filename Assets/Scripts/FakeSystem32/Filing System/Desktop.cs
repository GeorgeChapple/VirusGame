using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : To set up the desktop area with Icons in the desktop file directory file
*/

public class Desktop : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject emptySpacePrefab;
    [SerializeField] private GameObject windowsIconPrefab;
    public GridLayoutGroup grid;
    public List<GameObject> desktopSpaces = new List<GameObject>();

    [Tooltip("This var needs to have the desktop fileData file in it")]
    [SerializeField] private FileData deskTopFileDirectory;

    private GameEventsManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<GameEventsManager>();
    }
    // Sets up a grid of empty spaces on the desktop, for dropping the icon on whichever space on the screen is closest
    public void SetUpDesktopGrid()
    {
        // For if there are any objects in grid space already
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        int amountToSpawn = 0;

        float oneSpaceX = grid.cellSize.x + grid.spacing.x; // One space filled by icon and spacing of grid
        float oneSpaceY = grid.cellSize.y + grid.spacing.y;

        int numX = 0;
        float xVal = grid.spacing.x;
        while (xVal < canvas.pixelRect.width)
        {
            xVal += oneSpaceX; // Add space to val then check if its too far
            if (xVal >= canvas.pixelRect.width) { break; }
            numX++;
        } // Here i'm seeing how many icons fit width wise

        int numY = 0;
        float yVal = grid.spacing.x;
        while (yVal < canvas.pixelRect.height)
        {
            yVal += oneSpaceY;
            if (yVal >= canvas.pixelRect.height) { break; }
            numY++;
        } // Same but height wise

        amountToSpawn = numX * numY; // Spawn this many use for loop to do it

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject space = Instantiate(emptySpacePrefab, gameObject.transform);
            space.name = "EmptySpace " + i;
            desktopSpaces.Add(space);
        }
    }
    // Sets up a new windows icon button on the desktop area
    public void SetUpIcon(FileData file, int spotToSpawnIn)
    {
        GameObject obj = GameObject.Instantiate(windowsIconPrefab, desktopSpaces[spotToSpawnIn].transform);// Spawn button

        // Then set some variables
        obj.name = file.name;
        WindowsButton wbComp = obj.GetComponent<WindowsButton>();
        wbComp.SetUpVariables(file, file.application, obj.GetComponent<SpriteHandlerScript>());
        wbComp.SetUpVariables(file, file.application, file.sceneName, file.cameraMaterial);
        wbComp.canBeTaskbarIcon = file.canBeTaskBarIcon;
        wbComp.iconState = WindowsButton.IconState.Desktop;
        obj.GetComponentInChildren<TextMeshProUGUI>().text = file.name;
        obj.GetComponent<WindowsButton>().canBeDragged = file.canBeDragged;

        wbComp.file = file;


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
                    action = new UnityAction(delegate { obj.SendMessage(methodName, intVal); }); // Create new event which calls the methodname on every monobehaviour obj
                }
                else if (eventPass.passFloatVal)
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName, floatVal); });
                }
                else if (eventPass.passStringVal)
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName, stringVal); });
                }
                else if (eventPass.passSelfVal)
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName, selfVal); });
                }
                else
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName); });
                }
            }
            else
            {
                action = new UnityAction(delegate { obj.SendMessage(methodName); });
            }
            if (action != null) // Add it to the double click events
            {
                obj.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action);
            }
            manager.desktopIcons.Add(obj); // For if we need to access all of them
        }
    }
    public void SetUpDesktopSavedLayout()
    {
        int i = 0;

        // Clear the list to reset the desktop (for sequencing and if there were already things in there for whatever reason)
        manager.desktopIcons.Clear();

        // Destroy any existing icons from the desktop slots
        foreach (GameObject obj in desktopSpaces)
        {
            if (obj.transform.childCount > 0)
            {
                Destroy(obj.transform.GetChild(0).gameObject);
            }
        }

        // Spawn all icons the desktop has available
        foreach (FileData file in deskTopFileDirectory.children)
        {
            // Skip null entries and move to the next index, this will leave an empty space
            // thought i'd use it for saving files but i dont have time for that, maybe for GameX
            if (file == null)
            {
                i++;
                continue;
            }

            using (StreamReader sr = new StreamReader(manager.icons_SaveFilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(file.name) && line[0] == '1')
                    {
                        SetUpIcon(file, i);
                    }
                }
                i++;
            }
        }
    }

}
