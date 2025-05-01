using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
    Purpose           : For use with the raycast script.
                        It gives the raycast script the
                        variables to properly drag an item
                        and also window hierarchy stuff.
*/
public class WindowScript : MonoBehaviour
{
    public Vector2 mouseDistance;
    private BoxCollider windowCollider;
    private UnityEngine.RectTransform rectTransform;
    public FakeWindows32 manager;
    private Canvas canvas;

    private void Awake() {
        manager = GameObject.FindAnyObjectByType<FakeWindows32>();
        canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();
        windowCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<UnityEngine.RectTransform>();
        windowCollider.size = rectTransform.rect.size;
        if (gameObject.layer == 3) // If its a window - Jason
        {
            PutInFront();
        }
    }


    public void GetMouseDistance() {
        Object[] objects = FindObjectsOfType<RaycastScript>();
        RaycastScript script = (RaycastScript)objects[0];

        mouseDistance = Input.mousePosition - (Vector3)rectTransform.anchoredPosition;
    }
    public void PutInFront() // Put the window in front of everything - Jason
    {
        gameObject.transform.SetParent(manager.windowHierarchy.transform, true);
        gameObject.transform.SetAsFirstSibling();
        manager.OnHierarchyUpdated();
    }
    public void DetachFromHierarchy() // For when dragging, I want it to detach from the hierarchy to be in front of the other windows - Jason
    {
        gameObject.transform.SetParent(canvas.transform, true);
    }
    public void EnlargeCollider() {
        windowCollider.size *= 500f;
    }

    public void ResetCollider() {
        windowCollider.size = rectTransform.rect.size;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
    public UnityEngine.RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}