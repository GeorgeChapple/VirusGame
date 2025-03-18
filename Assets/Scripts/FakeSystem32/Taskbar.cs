using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class Taskbar : MonoBehaviour
{
    public List<GameObject> taskBarSpaces = new List<GameObject>();

    private void Awake()
    {
        //add icons to taskbarSpaces here
        //this will be player saved
    }

    public void SetUpTaskBarSpaces()
    {
        foreach (GameObject space in taskBarSpaces)
        {
            GameObject icon = Instantiate(space, gameObject.transform);
            icon.name = space.name + " Icon";
        }
    }
}
