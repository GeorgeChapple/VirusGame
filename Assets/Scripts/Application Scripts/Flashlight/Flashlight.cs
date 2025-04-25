using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Transform flashlightTransform;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private string tagToCheck;
    GameObject[] allTexts = new GameObject[0];

    private void Start()
    {
        flashlightTransform = transform;
        CheckAll(true);
    }
    private void OnDestroy()
    {
        CheckAll(false);
    }
    private void Update()
    {
        mousePosition = Input.mousePosition + new Vector3(0, 0, -1000);
        flashlightTransform.localPosition = mousePosition - (new Vector3(Screen.width, Screen.height) / 2);
    }
    private void CheckAll(bool open)
    {
        allTexts = GameObject.FindGameObjectsWithTag(tagToCheck);
        foreach (GameObject text in allTexts)
        {
            text.TryGetComponent(out FlashlightText flashlightText);
            flashlightText.flashlightOpen = open;
            flashlightText.ChangeText();
        }
    }
}
