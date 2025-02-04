using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Taskbar : MonoBehaviour
{
    private FakeWindows32 windows32;
    [SerializeField] private Transform gridStart;
    [SerializeField] private GameObject iconPrefab;

    public List<Vector3> taskBarSpaces;
    private void Awake()
    {
        windows32 = FindAnyObjectByType<FakeWindows32>();
        taskBarSpaces = windows32.CreateGridUI(gridStart, 10, 1, 30);
        AssignIconsToGrid();
    }


    private void AssignIconsToGrid()
    {
        foreach (var space in taskBarSpaces)
        {
            //GameObject icon = Instantiate(iconPrefab, space, Quaternion.identity, gameObject.transform);
            GameObject icon = Instantiate(iconPrefab, gameObject.transform);
            icon.GetComponent<RectTransform>().anchoredPosition = space;
        }
    }
}
