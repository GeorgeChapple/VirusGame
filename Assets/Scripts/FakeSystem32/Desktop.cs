using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class Desktop : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject emptySpacePrefab;
    [SerializeField] private GridLayoutGroup grid;
    public List<GameObject> desktopSpaces = new List<GameObject>();
    public List<GameObject> desktopIcons = new List<GameObject>();
    public void SetUpDesktopGrid()
    {
        //logic here for if there are any objects in grid space already


        int amountToSpawn = 0;

        float oneSpaceX = grid.cellSize.x + grid.spacing.x; //one space filled by icon and spacing of grid
        float oneSpaceY = grid.cellSize.y + grid.spacing.y;

        int numX = 0;
        float xVal = grid.spacing.x;
        while (xVal < canvas.pixelRect.width)
        {
            xVal += oneSpaceX; //add space to val then check if its too far
            if (xVal >= canvas.pixelRect.width) { break; }
            numX++;
        } //here i'm seeing how many icons fit width wise

        int numY = 0;
        float yVal = grid.spacing.x;
        while (yVal < canvas.pixelRect.height)
        {
            yVal += oneSpaceY;
            if (yVal >= canvas.pixelRect.height) { break; }
            numY++;
        } //same but height wise

        amountToSpawn = numX * numY; //spawn this many use for loop for it

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject space = Instantiate(emptySpacePrefab, gameObject.transform);
            space.name = "EmptySpace " + i;
            desktopSpaces.Add(space);
        }
    }
    public void SetUpDesktopSavedLayout()
    { //this will be player saved, dont forget to make a recycling bin after make file explorer
        foreach (var (space, i) in desktopIcons.Select((value, i) => (value, i)))
        {
            if (space != null)
            {
                GameObject.Instantiate(space, desktopSpaces[i].transform);
            }
        }
    }
}
