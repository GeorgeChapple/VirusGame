using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public Vector2 mouseDistance;
    private BoxCollider windowCollider;

    private void Awake() {
        windowCollider = GetComponent<BoxCollider>();
    }

    public void GetMouseDistance() {
        Object[] objects = FindObjectsOfType<RaycastScript>();
        RaycastScript script = (RaycastScript)objects[0];
        mouseDistance = script.lastHit.point - transform.position;
    }

    public void EnlargeCollider() {
        windowCollider.size *= 5000f;
    }

    public void ResetCollider() {
        windowCollider.size = new Vector3(1, 1, 0);
    }
}