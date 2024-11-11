using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Cube_Script : MonoBehaviour
{
    [SerializeField] private float numToMoveBy;
    [SerializeField] private bool useX;
    [SerializeField] private bool useY;
    [SerializeField] private bool useZ;
    void Update()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        if (useX)
        {
            x = numToMoveBy;
        }
        else if (useY)
        {
            y = numToMoveBy;
        }
        else if (useZ) 
        {
            z = numToMoveBy;
        }
        Vector3 posToAdd = new Vector3(x, y, z);
        transform.position += posToAdd;
    }
}
