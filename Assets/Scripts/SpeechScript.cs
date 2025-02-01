using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpeechScript : MonoBehaviour
{
    public SpeechManager manager;
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

    private IEnumerator WriteText() {
        manager.finishedWriting = false;
        for (int i = 0; i < text.Length; i++) {
            textBox.text += text[i];
            yield return new WaitForSeconds(textSpeed);
        }
        if (autoLineBreak) {
            textBox.text += "<br>";
        }
        manager.finishedWriting = true;
    }
}
