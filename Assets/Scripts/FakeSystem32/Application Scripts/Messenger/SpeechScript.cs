using System.Collections;
using TMPro;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class SpeechScript : MonoBehaviour
{
    [HideInInspector] public float textSpeed = 0.05f;
    [HideInInspector] public bool autoLineBreak = true;
    [HideInInspector] public string text;
    [HideInInspector] public SpeechManager speechManager;
    private TextMeshProUGUI textBox;

    private void Awake() {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartText() {
        StartCoroutine(WriteText());
    }

    // Displays currently stored string character by character
    private IEnumerator WriteText() {
        if (speechManager.skip)
        {
            textBox.text += text;
        } else
        {
            for (int i = 0; i < text.Length; i++)
            { // For each character in string, display character at index and wait for text speed time
                textBox.text += text[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
        if (autoLineBreak) { // Checks to see if auto line break is enabled, add a line break if yes
            textBox.text += "<br>";
        }
    }
}
