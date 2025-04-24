using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class ScrollRectNoDrag : ScrollRect
{
    public override void OnBeginDrag(PointerEventData eventData) { }
    public override void OnDrag(PointerEventData eventData) { }
    public override void OnEndDrag(PointerEventData eventData) { }

    //Figured I could stop it from dragging if I just override the functions
    //Needed to do this so i could drag objects out of it for the virus scanner
}