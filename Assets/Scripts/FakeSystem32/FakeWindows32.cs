using System.Collections.Generic;
using UnityEngine;

public class FakeWindows32 : MonoBehaviour
{
    [Header("Settings")]
    private int gridSpacing;

    [Header("Serialisations")]
    [SerializeField] private Taskbar taskBar;
    [SerializeField] private Desktop desktop;


    //this will set up windows
    //load previously saved icon positions
    //set up
    private void Awake()
    {
        desktop.SetUpDesktopGrid();
        taskBar.SetUpTaskBarSpaces();
        desktop.SetUpDesktopSavedLayout();
    }


    private void Update()
    {
        //
    }

}
