using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpeechManager : MonoBehaviour {
    [SerializeField] public List<GameObject> speechPrefabs = new List<GameObject>();
    [SerializeField] private TextAsset file;
    public List<GameObject> speechBubbles = new List<GameObject>();
    public bool next;
    public bool finishedWriting = false;
    public UnityEvent newBubbleCreated;
    [SerializeField] private int speechPrafabsIndex = 0;
    private float textSpeed = 0.05f;
    private bool autoLineBreak = true;
    private string filePath;
    private string errorMessage = " <br>***TEXT BOX SYNTAX ERROR*** <br>";

    public void StartTextLoop(TextAsset inputFile) {
        GetNewTextFile(inputFile);
        StartCoroutine(TextLoop());
    }

    private void GetNewTextFile(TextAsset inputFile) {
        file = inputFile;
        filePath = AssetDatabase.GetAssetPath(file);
    }

    private string GetNewValFromCommandString(string line) {
        string newVal = "";
        for (int i = newVal.IndexOf(':') + 1; i < line.Length; i++) {
            if (line[i] != '>') {
                newVal += line[i];
            }
        }
        return newVal;
    }

    private IEnumerator TextLoop() {
        bool continueRead;
        bool newBubble;
        speechBubbles.Add(Instantiate(speechPrefabs[speechPrafabsIndex]));
        newBubbleCreated.Invoke();
        using (StreamReader sr = new StreamReader(filePath)) {
            string line;
            string nextLine = "";
            while ((line = sr.ReadLine()) != null) {
                yield return new WaitForSeconds(nextLine.Length * textSpeed + 0.1f);
                continueRead = true;
                newBubble = false;
                nextLine = "";
                if (line == "<n>") {
                    continueRead = false;
                    newBubble = true;
                } else if (line == "<p>") {
                    continueRead = false;
                } else if (line.Length >= 4 && line.EndsWith(">") && line.Substring(0, 4) == "<sp:") {
                    string newSpeed = GetNewValFromCommandString(line);
                    try {
                        textSpeed = float.Parse(newSpeed);
                    } catch {
                        nextLine += errorMessage;
                        nextLine += "~~~SPEED INVALID VALUE~~~ <br>";
                    }
                } else if (line.Length >= 5 && line.EndsWith(">") && line.Substring(0, 5) == "<alb:") {
                    if (line[5] == '0') {
                        autoLineBreak = false;
                    } else if (line[5] == '1') {
                        autoLineBreak = true;
                    } else {
                        nextLine += errorMessage;
                        nextLine += "~~~AUTO LINE BREAK INVALID VALUE~~~ <br>";
                    }
                } else if (line.Length >= 5 && line.EndsWith(">") && line.Substring(0, 5) == "<pfb:") {
                    string newPrefabIndexString = GetNewValFromCommandString(line);
                    int newPrefabIndex = 0;
                    try {
                        newPrefabIndex = Convert.ToInt32(newPrefabIndex);
                        if (newPrefabIndex >= speechPrefabs.Count) {
                            nextLine += errorMessage;
                            nextLine += "~~~PREFAB INDEX OUT OF BOUNDS~~~ <br>";
                        } else {
                            speechPrafabsIndex = newPrefabIndex;
                        }
                    } catch {
                        nextLine += errorMessage;
                        nextLine += "~~~PREFAB INDEX INVALID VALUE~~~ <br>";
                    }
                } else {
                    nextLine += line;
                }
                SpeechScript currentBubble = speechBubbles[speechBubbles.Count - 1].GetComponent<SpeechScript>();
                currentBubble.manager = this;
                currentBubble.textSpeed = textSpeed;
                currentBubble.autoLineBreak = autoLineBreak;
                currentBubble.text = nextLine;
                currentBubble.StartText();
                while (!continueRead) {
                    if (next) {
                        if (newBubble) {
                            speechBubbles.Add(Instantiate(speechPrefabs[speechPrafabsIndex]));
                            newBubbleCreated.Invoke();
                        }
                        next = false;
                        continueRead = true;
                    }
                    yield return null;
                }
                yield return null;
            }
        }
    }
}
