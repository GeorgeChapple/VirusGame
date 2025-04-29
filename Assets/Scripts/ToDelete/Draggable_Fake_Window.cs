using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class Draggable_Fake_Window : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    private UnityEngine.RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<UnityEngine.RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}

