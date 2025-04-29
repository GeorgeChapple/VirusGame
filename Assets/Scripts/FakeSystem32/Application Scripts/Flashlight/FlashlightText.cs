using TMPro;
using UnityEngine;

public class FlashlightText : MonoBehaviour
{
    [HideInInspector]
    public bool flashlightOpen = false;
    private TextMeshProUGUI selfText;
    [SerializeField] private TextMeshProUGUI textToDisable;
    public GenericEventHandler eventHandler;
    private BoxCollider boxCollider;
    private RectTransform rectTransform;

    void Awake()
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
    public void ChangeText()
    {
        
        selfText.enabled = flashlightOpen;
        //boxCollider.size = new Vector3(rectTransform.rect.size.x, rectTransform.rect.size.y, 2);
        textToDisable.enabled = !flashlightOpen;
        
    }
    private void LateUpdate()
    {
        if (boxCollider != null)
        {
            boxCollider.size = new Vector3(rectTransform.rect.size.x, rectTransform.rect.size.y, 2);
        }
        //hate that but it wasnt working in change text
    }
}
