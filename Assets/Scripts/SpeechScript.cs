using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpeechScript : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    public float textSpeed = 0.1f;
    private string filePath;
    private string errorMessage = " <br>***TEXT BOX SYNTAX ERROR*** <br>";
    private TextMeshProUGUI textBox;
    public bool autoLineBreak = true;

    public bool forceSkip;

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
                } else if (line.Length >= 4 && line.EndsWith(">") && line.Substring(0, 4) == "<sp:") {
                    string newSpeed = "";
                    for (int i = 4; i < line.Length; i++) {
                        if (line[i] != '>') {
                            newSpeed += line[i];
                        } else {
                            try {
                                textSpeed = float.Parse(newSpeed);
                            } catch {
                                textBox.text += errorMessage;
                                textBox.text += "~~~SPEED INVALID VALUE~~~ <br>";
                                textSpeed = 0.1f;
                            }
                        }
                    }
                } else if (line.Length >= 5 && line.EndsWith(">") && line.Substring(0, 5) == "<alb:") {
                    if (line[5] == '0') {
                        autoLineBreak = false;
                    } else if (line[5] == '1') {
                        autoLineBreak = true;
                    } else {
                        textBox.text += errorMessage;
                        textBox.text += "~~~AUTO LINE BREAK INVALID VALUE~~~ <br>";
                    }
                } else {
                    for (int i = 0; i < line.Length; i++) {
                        textBox.text += line[i];
                        yield return new WaitForSeconds(textSpeed);
                    }
                    if (autoLineBreak) {
                        textBox.text += " <br>";
                    }
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
