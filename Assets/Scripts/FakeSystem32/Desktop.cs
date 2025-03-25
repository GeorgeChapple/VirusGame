using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

            //obj.GetComponent<HitEventScript>().doubleHitEvent.AddListener(delegate { file.OnDoubleClick.G})

            //deconstruct persistent event
            for (int j = 0; j < file.OnDoubleClick.GetPersistentEventCount(); j++)
            {
                
                string target = file.OnDoubleClick.GetPersistentTarget(j).ToString();
                string[] targetArray = target.Split('(', ')');
                target = targetArray[1].Trim();

                string methodName = file.OnDoubleClick.GetPersistentMethodName(j).ToString();

                string function = "obj.GetComponent<" + target + ">()." + methodName + "(1);";
                Debug.Log(function);

                UnityAction newEvent = new UnityAction(delegate { obj.GetComponent<SpriteHandlerScript>().SetSpriteIndex(1); });
                //UnityAction newEvent = new UnityAction(delegate { Invoke(methodName, 0f); });

                obj.GetComponent<HitEventScript>().doubleHitEvent.AddListener(newEvent);
            }

            //if(file.OnDoubleClick.GetPersistentEventCount() > 0)
            //{
            //    Debug.Log(file.OnDoubleClick.GetPersistentTarget(0));
            //    Debug.Log(file.OnDoubleClick.GetPersistentMethodName(0));
            //}

            //Debug.Log(file.OnDoubleClick.GetPersistentMethodName(0));
            //MethodInfo targetInfo = UnityEvent.GetValidMethodInfo(this, nameof(TestCall), new Type[0]);
            //UnityAction methodDelegate = Delegate.CreateDelegate(typeof(UnityAction), this, targetInfo) as UnityAction;
            //UnityEventTools.AddPersistentListener(ue, methodDelegate);


            //obj.GetComponent<HitEventScript>().doubleHitEvent = file.OnDoubleClick;

            i++;
        }
    }
}
