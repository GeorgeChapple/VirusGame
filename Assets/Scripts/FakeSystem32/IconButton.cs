using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    public Canvas canvas;
    public GridLayoutGroup layoutGroup;
    private void Awake()
    {
        if(canvas == null)
        {
            canvas = GameObject.Find("FakeWindows").GetComponent<Canvas>();
            layoutGroup = GameObject.Find("Desktop").GetComponent<GridLayoutGroup>();
            GetComponent<RectTransform>().sizeDelta = layoutGroup.cellSize;
            GetComponent<BoxCollider>().size = layoutGroup.cellSize;
        }
    }
    public void MoveToTopOfHierarchy()
    {
        gameObject.transform.SetParent(canvas.transform, true);
    }

    public void TestDouble()
    {
        print("Double Click!!");
    }
}
