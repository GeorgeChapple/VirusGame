using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : Swaps between two versions of text, a non-coloured version and a coloured version that will add to the puzzle.
                        only works if flashlight is out.
*/
public class FlashlightText : MonoBehaviour
{
    [HideInInspector]
    public bool flashlightOpen = false;
    private TextMeshProUGUI selfText;
    [SerializeField] private TextMeshProUGUI textToDisable;
    public GenericEventHandler eventHandler;
    private BoxCollider boxCollider;
    private RectTransform rectTransform;

    void Awake() // Set variables and check if flashlight is open
    {
        selfText = GetComponent<TextMeshProUGUI>();
        boxCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<RectTransform>();

        if (selfText != null)
        {
            flashlightOpen = GameObject.Find("Flashlight");

        }
        if (flashlightOpen)
        {
            eventHandler.ByScriptEvent.Invoke();
        }
        ChangeText();
    }
    public void ChangeText() // Swaps text out if flash light open
    {
        selfText.enabled = flashlightOpen;
        textToDisable.enabled = !flashlightOpen;
        
    }
    private void LateUpdate()
    {
        if (boxCollider != null) // Set size properly
        {
            boxCollider.size = new Vector3(rectTransform.rect.size.x, rectTransform.rect.size.y, 2);
        }
    }
}
