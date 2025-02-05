using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject emptySpacePrefab;
    [SerializeField] private GridLayoutGroup grid;
    public List<GameObject> spaces = new List<GameObject>();

    private void Awake()
    {
        int amountToSpawn = 0;

        float oneSpaceX = grid.cellSize.x + grid.spacing.x; //one space filled by icon and spacing of grid
        float oneSpaceY = grid.cellSize.y + grid.spacing.y;

        int numX = 0;
        float xVal = grid.spacing.x;
        while (xVal < canvas.pixelRect.width)
        {
            xVal += oneSpaceX;
            numX++;
        } //here i'm seeing how many icons fit width wise
        
        int numY = 0;
        float yVal = grid.spacing.x;
        while (yVal < canvas.pixelRect.height)
        {
            yVal += oneSpaceY;
            numY++;
        } //same but height wise

        numX -= 1; //need this for now but i should if i tweak the numbers a little (i gotta go rn)
        numY -= 1;

        amountToSpawn = numX * numY; //spawn this many \/ for loop for it

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject space = Instantiate(emptySpacePrefab, gameObject.transform);
            space.name = "EmptySpace " + i;
            spaces.Add(space);
        }
    }
}
