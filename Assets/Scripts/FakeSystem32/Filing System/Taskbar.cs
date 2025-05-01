using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : To set up the taskbar area with Icons in the taskbar file directory file
*/

public class Taskbar : MonoBehaviour
{
    public GridLayoutGroup grid;
    public GameObject taskBarIconPrefab;

    [Tooltip("This var needs to have the taskbar fileData file in it")]
    [SerializeField] private FileData taskBarFileDirectory;

    [SerializeField] private List<GameObject> taskbarSpaces = new List<GameObject>();

    // Sets up a new windows icon button on the taskbar area
    public void SetUpTaskbarIcon(FileData file)
    {
        GameObject icon = Instantiate(taskBarIconPrefab, gameObject.transform); // Spawn Button

        // Force box collider size because it wasnt setting automatically
        icon.GetComponent<BoxCollider>().size = new Vector3(grid.cellSize.x, grid.cellSize.y, 1); 

        taskbarSpaces.Add(icon); // Incase we needed to access all icons on taskbar

        // Set a bunch of variables
        icon.name = file.name + " Icon";
        WindowsButton wbComp = icon.GetComponent<WindowsButton>();
        wbComp.SetUpVariables(file, file.application, icon.GetComponent<SpriteHandlerScript>());
        wbComp.SetUpVariables(file, file.application, file.sceneName, file.cameraMaterial);
        wbComp.canBeDragged = file.canBeDragged;
        wbComp.canBeTaskbarIcon = file.canBeTaskBarIcon;
        wbComp.iconState = WindowsButton.IconState.Taskbar;
        wbComp.file = file;

        // Make text invisible so that once it becomes a desktop icon it appears again
        TextMeshProUGUI text = icon.GetComponentInChildren<TextMeshProUGUI>();
        text.text = file.name;
        Color transparentCol = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.color = transparentCol;

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
                    action = new UnityAction(delegate { icon.SendMessage(methodName, intVal); }); // Create new event which calls the methodname on every monobehaviour obj
                }
                else if (eventPass.passFloatVal)
                {
                    action = new UnityAction(delegate { icon.SendMessage(methodName, floatVal); });
                }
                else if (eventPass.passStringVal)
                {
                    action = new UnityAction(delegate { icon.SendMessage(methodName, stringVal); });
                }
                else if (eventPass.passSelfVal)
                {
                    action = new UnityAction(delegate { icon.SendMessage(methodName, selfVal); });
                }
                else
                {
                    action = new UnityAction(delegate { icon.SendMessage(methodName); });
                }
            }
            else
            {
                action = new UnityAction(delegate { icon.SendMessage(methodName); });
            }
            if (action != null) // Add it to the double click events
            {
                icon.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action);
            }
        }
    }
    // Sets up all buttons on the taskbar as needed
    public void SetUpTaskBarSpaces()
    {
        foreach (FileData file in taskBarFileDirectory.children)
        {
            SetUpTaskbarIcon(file);
        }
    }
}
