using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpeechManager : MonoBehaviour {
    public List<GameObject> speechPrefabs = new List<GameObject>(); // Stores the prefab speech bubbles
    public List<GameObject> speechBubbles = new List<GameObject>(); // Stores all currently spawned speechBubbles
    public TextAsset file; // Stores the text file to be read and displayed
    public UnityEvent newBubbleCreated;
    public bool next;
    [SerializeField] private int speechPrafabsIndex = 0;
    private float textSpeed = 0.05f;
    private bool autoLineBreak = true;
    private string filePath;
    private string errorMessage = " <br>***TEXT BOX SYNTAX ERROR*** <br>";
    
    // Grabs file path and starts the process of reading it
    public void StartTextLoop(TextAsset inputFile) {
        GetNewTextFile(inputFile);
        StartCoroutine(TextLoop());
    }

    // Finds the file path of the given .txt file
    private void GetNewTextFile(TextAsset inputFile) {
        file = inputFile;
        filePath = AssetDatabase.GetAssetPath(file);
    }

    // Called when a command line is read that needs to return a number of some kind, returns the string version of just the number in the command
    private string GetNewValFromCommandString(string line) {
        string newVal = "";
        for (int i = newVal.IndexOf(':') + 1; i < line.Length; i++) {
            if (line[i] != '>') {
                newVal += line[i];
            }
        }
        return newVal;
    }

    // Reads the current stored .txt file line by line, processing commands
    private IEnumerator TextLoop() {
        bool continueRead;
        bool newBubble;
        speechBubbles.Add(Instantiate(speechPrefabs[speechPrafabsIndex])); // Instantiates the first speech bubble, using the default index
        newBubbleCreated.Invoke();
        using (StreamReader sr = new StreamReader(filePath)) { // Opens a stream reader, closes the stream reader when done
            string line;
            string nextLine = "";
            while ((line = sr.ReadLine()) != null) { // Reads the file line by line until there are no more lines
                yield return new WaitForSeconds(nextLine.Length * textSpeed + 0.01f); // Waits until previous line has finished being displayed, prevents text being overwritten
                continueRead = true; // If continue read is set to false in if statements, reading will pause
                newBubble = false; // If set to true by if statements, a new speech bubble will be spawned, which will be used by the next line of text
                nextLine = "";
                // Checks the next line, determines what command it is, runs the desired command, if no command is present, line is text and is sent to the speech bubble to be written
                if (line == "<n>") { // Checks for new speech bubble command, tells loop to spawn a new speech bubble 
                    continueRead = false;
                    newBubble = true;
                } else if (line == "<p>") { // Checks for pause command, tells loop to pause reading
                    continueRead = false;
                } else if (line.Length >= 4 && line.EndsWith(">") && line.Substring(0, 4) == "<sp:") { // Checks for speed command, tells loop to change text speed to the input value
                    string newSpeed = GetNewValFromCommandString(line); // grabs string version of value in the command
                    try { // Try to parse the string into a float, make this the new text speed
                        textSpeed = float.Parse(newSpeed);
                    } catch { // If fails, input is invalid, display error message
                        nextLine += errorMessage;
                        nextLine += "~~~SPEED INVALID VALUE~~~ <br>";
                    }
                } else if (line.Length >= 5 && line.EndsWith(">") && line.Substring(0, 5) == "<alb:") { // Checks for auto line break command, tells loop to enable/disable auto line breaking based on input value
                    if (line[5] == '0') { // If input value is 0, disable ALB
                        autoLineBreak = false;
                    } else if (line[5] == '1') { // If 1, enable ALB
                        autoLineBreak = true;
                    } else { // If input is anything else, display error message
                        nextLine += errorMessage;
                        nextLine += "~~~AUTO LINE BREAK INVALID VALUE~~~ <br>";
                    }
                } else if (line.Length >= 5 && line.EndsWith(">") && line.Substring(0, 5) == "<pfb:") { // Checks for prefab index command, tells loop to change the selected speech bubble prefab
                    string newPrefabIndexString = GetNewValFromCommandString(line);
                    int newPrefabIndex = 0;
                    try { // Try to convert value into index
                        newPrefabIndex = Convert.ToInt32(newPrefabIndex);
                        if (newPrefabIndex >= speechPrefabs.Count) { // Check if new index is outside bounds of prefab list, if yes, display error message
                            nextLine += errorMessage;
                            nextLine += "~~~PREFAB INDEX OUT OF BOUNDS~~~ <br>";
                        } else { // If within range, set new index 
                            speechPrafabsIndex = newPrefabIndex;
                        }
                    } catch { // Display error message if failed to covert value to index
                        nextLine += errorMessage;
                        nextLine += "~~~PREFAB INDEX INVALID VALUE~~~ <br>";
                    }
                } else { // If line has no commands, it is text to be read, sets next line to current line
                    nextLine += line;
                }
                SpeechScript currentBubble = speechBubbles[speechBubbles.Count - 1].GetComponent<SpeechScript>(); // Sets current speech bubble to last speech bubble stored in list (which would be the latest one spawned in)
                // Set the values in speech bubble to those of the manager, keeps speech bubble/newly spawned bubbles up with the correct settings set by the text file
                currentBubble.textSpeed = textSpeed;
                currentBubble.autoLineBreak = autoLineBreak;
                currentBubble.text = nextLine;
                currentBubble.StartText(); // Starts writing the line of text
                while (!continueRead) { // Checks if loop set to pause, pauses when true, waits to be told to go next
                    if (next) {
                        if (newBubble) { // If new bubble needs to be spawned, spawn a new speech bubble, unpause
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
