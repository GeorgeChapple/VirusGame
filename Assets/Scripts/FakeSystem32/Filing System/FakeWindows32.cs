using System;
using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : Essentially the whole systems manager, this was supposed 
                        to do more but didnt get around to it really since 
                        it was already split between everything.
*/
public class FakeWindows32 : MonoBehaviour
{
    [Header("Serialisations")]
    [SerializeField] private Taskbar taskBar;
    public Desktop desktop;

    [SerializeField] private FileData fileDirectories;

    [SerializeField] private TextMeshProUGUI timeNDate;

    public GameObject windowHierarchy;

    private void Awake()
    {
        //force resolution 1920x1080 to keep canvas spacing(i dont really want to but i dont know how to scale all of this properly, maybe this will be for later)
        Screen.SetResolution(1920, 1080, true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Set Up whole system
    private void Start() 
    { 
        desktop.SetUpDesktopGrid();

        desktop.SetUpDesktopSavedLayout();

        taskBar.SetUpTaskBarSpaces();
    }

    private void Update() // Get current date and time
    {
        string newTimeNDate = "";
        newTimeNDate = new string(DateTime.Now.ToString());
        timeNDate.text = newTimeNDate;
    }
    public void OnHierarchyUpdated() // Move windows into hierarchy based on position as children
    {
        for (int i = 0; i < windowHierarchy.transform.childCount; i++)
        {
            GameObject window = windowHierarchy.transform.GetChild(i).gameObject;
            window.transform.position = new(window.transform.position.x, window.transform.position.y, (float)i);
        }
    }
}