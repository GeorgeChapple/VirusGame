using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : For use with the websites, it would show secrets hidden in the website.
                        There was talk for the use of this in other things i believe
                        but we hadn't done any of that unfortunately as we had heavily overscoped.
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
        // Move flashlight graphic over mouse position
        mousePosition = Input.mousePosition + new Vector3(0, 0, -500);
        flashlightTransform.localPosition = mousePosition - (new Vector3(Screen.width, Screen.height) / 2);
    }
    // Check all objects that should show a different text
    private void CheckAll(bool open)
    {        
        allTexts = GameObject.FindGameObjectsWithTag(tagToCheck);
        foreach (GameObject text in allTexts)
        {
            text.TryGetComponent(out FlashlightText flashlightText);
            flashlightText.flashlightOpen = open;
            flashlightText.ChangeText();
            flashlightText.GetComponent<GenericEventHandler>().ByScriptEvent.Invoke();
        }
    }
}
