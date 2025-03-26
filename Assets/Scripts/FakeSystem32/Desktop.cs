using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class Desktop : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject emptySpacePrefab;
    [SerializeField] private GameObject windowsIconPrefab;
    [SerializeField] private GridLayoutGroup grid;
    public List<GameObject> desktopSpaces = new List<GameObject>();
    public List<GameObject> desktopIcons = new List<GameObject>();
    public Dictionary<GameObject, FileData> dictionary = new Dictionary<GameObject, FileData>();

    [Tooltip("This var needs to have the desktop fileData file in it")]
    [SerializeField] private FileData deskTopFileDirectory;
    public void SetUpDesktopGrid()
    {
        //logic here for if there are any objects in grid space already


        int amountToSpawn = 0;

        float oneSpaceX = grid.cellSize.x + grid.spacing.x; //one space filled by icon and spacing of grid
        float oneSpaceY = grid.cellSize.y + grid.spacing.y;

        int numX = 0;
        float xVal = grid.spacing.x;
        while (xVal < canvas.pixelRect.width)
        {
            xVal += oneSpaceX; //add space to val then check if its too far
            if (xVal >= canvas.pixelRect.width) { break; }
            numX++;
        } //here i'm seeing how many icons fit width wise

        int numY = 0;
        float yVal = grid.spacing.x;
        while (yVal < canvas.pixelRect.height)
        {
            yVal += oneSpaceY;
            if (yVal >= canvas.pixelRect.height) { break; }
            numY++;
        } //same but height wise

        amountToSpawn = numX * numY; //spawn this many use for loop for it

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject space = Instantiate(emptySpacePrefab, gameObject.transform);
            space.name = "EmptySpace " + i;
            desktopSpaces.Add(space);
        }
    }
    public void SetUpDesktopSavedLayout()
    {
        int i = 0;
        foreach (FileData file in deskTopFileDirectory.children)
        {
            //FileData fileInstance = ScriptableObject.CreateInstance<FileData>();
            
            GameObject obj = GameObject.Instantiate(windowsIconPrefab, desktopSpaces[i].transform);
            obj.name = file.name;
            //fileInstance.self = obj;
            obj.GetComponent<WindowsButton>().SetUpVariables(file, file.application, obj.GetComponent<SpriteHandlerScript>());

            foreach (EventPass eventPass in file.OnDoubleClick)
            {
                UnityAction action;
                string methodName = eventPass.methodName; //get event values out
                int intVal = eventPass.intVal;
                float floatVal = eventPass.floatVal;
                if (eventPass.passValThrough)
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName, intVal); }); //create new event which calls the methodname on every monobehaviour obj
                }
                else
                {
                    action = new UnityAction(delegate { obj.SendMessage(methodName); });
                }
                if (action != null)
                {
                obj.GetComponent<HitEventScript>().doubleHitEvent.AddListener(action); //add it to the events
                }

            }
            
            i++;
        }
        
    }
    public void TestCall()
    {
        Debug.Log("called");
    }
}
