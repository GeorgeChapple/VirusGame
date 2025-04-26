using TMPro;
using UnityEngine;

public class FlashlightText : MonoBehaviour
{
    [HideInInspector] 
    public bool flashlightOpen = false;
    private TextMeshProUGUI selfText;
    [SerializeField] private TextMeshProUGUI textToDisable;
    public GenericEventHandler eventHandler;

    void Awake()
    {
        selfText = GetComponent<TextMeshProUGUI>();
        
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
        textToDisable.enabled = !flashlightOpen;
    }
}
