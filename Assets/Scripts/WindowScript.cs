using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public Vector2 mouseDistance;

    public void GetMouseDistance() {
        Object[] objects = Object.FindObjectsOfType<RaycastScript>();
        RaycastScript script = (RaycastScript)objects[0];
        mouseDistance = script.lastHit.point - transform.position;
    }
}
