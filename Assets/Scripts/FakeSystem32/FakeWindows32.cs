using System.Collections.Generic;
using UnityEngine;

public class FakeWindows32 : MonoBehaviour
{
    [Header("Serialisations")]
    [SerializeField] private Taskbar taskBar;
    [SerializeField] private Desktop desktop;

    //set up desktop and taskbar
    //load previously saved icon positions
    //

    private void Awake()
    {
        //force resolution 1920x1080 to keep canvas spacing(i dont really want to but i dont know how to scale all of this properly, maybe this will be for later)
        Screen.SetResolution(1920, 1080, true);

        desktop.SetUpDesktopGrid();
        taskBar.SetUpTaskBarSpaces();
        desktop.SetUpDesktopSavedLayout();
    }
}
