using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
            icon.GetComponent<Image>().sprite = file.icon[0];
        }
    }
}
