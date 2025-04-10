using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class Taskbar : MonoBehaviour
{
    //public List<GameObject> taskBarSpaces = new List<GameObject>();
    public GameObject taskBarIconPrefab;

    [Tooltip("This var needs to have the taskbar fileData file in it")]
    [SerializeField] private FileData taskBarFileDirectory;

    public void SetUpTaskBarSpaces()
    {
        foreach (FileData file in taskBarFileDirectory.children)
        {
            GameObject icon = Instantiate(taskBarIconPrefab, gameObject.transform);
            icon.name = file.name + " Icon";

            //icon.GetComponent<Image>().sprite = file.icon[0];
            icon.GetComponent<WindowsButton>().SetUpVariables(file, file.application, icon.GetComponent<SpriteHandlerScript>());
            icon.GetComponent<WindowsButton>().SetUpVariables(file, file.application, file.sceneIndex, file.cameraMaterial);


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
                        action = new UnityAction(delegate { icon.SendMessage(methodName, intVal); }); //create new event which calls the methodname on every monobehaviour obj
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
                if (action != null)
                {
                    icon.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action); //add it to the events
                }
            }
        }
    }
}
