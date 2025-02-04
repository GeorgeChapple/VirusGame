using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpeechScript : MonoBehaviour
{
    public float textSpeed = 0.05f;
    public bool autoLineBreak = true;
    public string text;
    private TextMeshProUGUI textBox;

    private void Awake() {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartText() {
        StartCoroutine(WriteText());
    }

    // Displays currently stored string character by character
    private IEnumerator WriteText() {
        for (int i = 0; i < text.Length; i++) { // For each character in string, display character at index and wait for text speed time
            textBox.text += text[i];
            yield return new WaitForSeconds(textSpeed);
        }
        if (autoLineBreak) { // Checks to see if auto line break is enabled, add a line break if yes
            textBox.text += "<br>";
        }
    }
}
