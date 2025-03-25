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

    [Tooltip("This var needs to have the taskbar fileData file in it")]
    [SerializeField] private FileData taskBarFileDirectory;

    public void SetUpTaskBarSpaces()
    {
        taskBarSpaces.Clear();
        foreach (FileData file in taskBarFileDirectory.children)
        {
            GameObject icon = Instantiate(file.self, gameObject.transform);
            icon.name = file.name + " Icon";
        }
    }
}
