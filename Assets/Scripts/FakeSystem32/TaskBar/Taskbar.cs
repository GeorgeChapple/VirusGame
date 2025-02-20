using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Taskbar : MonoBehaviour
{
    private FakeWindows32 windows32;

    public List<GameObject> taskBarSpaces = new List<GameObject>();

    private void Awake()
    {
        //add icons to taskbarSpaces here
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
