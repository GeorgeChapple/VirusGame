using System.Collections.Generic;
using UnityEngine;

public class FakeWindows32 : MonoBehaviour
{
    [Header("Settings")]
    private int gridSpacing;

    [Header("Serialisations")]
    [SerializeField] private Transform taskBar;


    //this will be the task bar
    //create a 1 by infinity grid for the icons to sit in
    //windows icon and utility bar will be already in scene with different scripts
    //icon grid will work in here
    //everything else will run seperate


    private void Update()
    {
        //handle things like drag and drop in here
    }

    public List<Vector3> CreateGridUI(Transform gridStart, int width, int height, float spacing) //width or height has to be mininum of 1
    {
        //make the grid

        List<Vector3> listSpaces = new List<Vector3>();

        //listSpaces.Add(gridStart.GetComponent<RectTransform>().anchoredPosition);

        for (int x = 1; x <= width; x++) //create width
        {
            for (int y = 0; y < height; y++) //create height
            {
                listSpaces.Add(new Vector3((x + gridStart.GetComponent<RectTransform>().anchoredPosition.x) * spacing, (y + gridStart.GetComponent<RectTransform>().anchoredPosition.y) * spacing, 0));
            }
        }

        return listSpaces;
    }
    public void UpdateGridUI()
    {
        //update the grid
        //like if stuff is dropped into it
        //
    }
}
