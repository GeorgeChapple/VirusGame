using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/
public class WindowScript : MonoBehaviour
{
    public Vector2 mouseDistance;
    private BoxCollider windowCollider;
    private UnityEngine.RectTransform rectTransform;

    private void Awake() {
        windowCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<UnityEngine.RectTransform>();
        windowCollider.size = rectTransform.rect.size;
    }

    public void GetMouseDistance() {
        Object[] objects = FindObjectsOfType<RaycastScript>();
        RaycastScript script = (RaycastScript)objects[0];

        mouseDistance = Input.mousePosition - (Vector3)rectTransform.anchoredPosition;
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