using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Transform flashlightTransform;
    [SerializeField] private Vector3 mousePosition;

    private void Start()
    {
        flashlightTransform = transform;
    }
    private void Update()
    {
        mousePosition = Input.mousePosition + new Vector3(0, 0, -1000);
        flashlightTransform.localPosition = mousePosition - (new Vector3(Screen.width,Screen.height) /2);
    }

}
