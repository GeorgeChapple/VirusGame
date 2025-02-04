using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FakeWindows32 : MonoBehaviour
{
    [Header("Settings")]
    private int gridSpacing;

    [Header("Serialisations")]
    [SerializeField] private Transform gridStart;


    //this will be the task bar
    //create a 1 by infinity grid for the icons to sit in
    //windows icon and utility bar will be already in scene with different scripts
    //icon grid will work in here
    //everything else will run seperate


    private void Update()
    {
        //handle things like drag and drop in here
    }

    private void CreateGrid()
    {
        //make the grid
    }

}
