using System;
using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
*/
public class FakeWindows32 : MonoBehaviour
{
    [Header("Serialisations")]
    [SerializeField] private Taskbar taskBar;
    [SerializeField] private Desktop desktop;

    [SerializeField] private FileData fileDirectories;

    [SerializeField] private TextMeshProUGUI timeNDate;

    public GameObject windowHierarchy;

    private void Awake()
    {
        //force resolution 1920x1080 to keep canvas spacing(i dont really want to but i dont know how to scale all of this properly, maybe this will be for later)
        Screen.SetResolution(1920, 1080, true);
    }

    private void Start() {
        desktop.SetUpDesktopGrid();

        desktop.SetUpDesktopSavedLayout();

        taskBar.SetUpTaskBarSpaces();
    }

    private void Update()
    {
        string newTimeNDate = "";
        newTimeNDate = new string(DateTime.Now.ToString());
        timeNDate.text = newTimeNDate;
    }
    public void OnHierarchyUpdated()
    {
        for (int i = 0; i < windowHierarchy.transform.childCount; i++)
        {
            GameObject window = windowHierarchy.transform.GetChild(i).gameObject;
            window.transform.position = new(window.transform.position.x, window.transform.position.y, (float)i);
        }
    }
}