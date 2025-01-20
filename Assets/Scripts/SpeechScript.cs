using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpeechScript : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    private float textSpeed = 0.1f;
    private string filePath;
    private TextMeshProUGUI textBox;

    [SerializeField] private bool forceSkip;

    void Awake() {
        filePath = AssetDatabase.GetAssetPath(file);
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(TextLoop());
    }

    private IEnumerator TextLoop() {
        bool continueRead;
        using (StreamReader sr = new StreamReader(filePath)) {
            string line;
            while ((line = sr.ReadLine()) != null) {
                continueRead = true;
                if (line == "<p>") {
                    continueRead = false;
                } else if (line.Substring(0, 4) == "<sp:" && line.EndsWith(">")) {
                    string newSpeed = "";
                    for (int i = 4; i < line.Length; i++) {
                        if (line[i] != '>') {
                            newSpeed += line[i];
                        } else {
                            textSpeed = float.Parse(newSpeed);
                        }
                    }
                } else {
                    for (int i = 0; i < line.Length; i++) {
                        textBox.text += line[i];
                        yield return new WaitForSeconds(textSpeed);
                    }
                    textBox.text += " <br>";
                }
                while (!continueRead) { 
                    if (forceSkip) {
                        forceSkip = false;
                        continueRead = true;
                    }
                    yield return null;
                }
                yield return null;
            }
        }
    }
}
