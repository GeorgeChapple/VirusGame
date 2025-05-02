using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

[CustomEditor(typeof(GameEventsManager))]
public class GameEventsManager_Editor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameEventsManager manager = (GameEventsManager)target;

        EditorGUILayout.LabelField(new GUIContent("Save Data Options", "For inspector/editor use only. Change the current file index and begin reading."));

        if (GUILayout.Button(new GUIContent("Set Save Files", "ASK BEFORE YOU PRESS THIS BUTTON! Sets up save data files/sets save data files to default values."))) {
            using (StreamWriter sw = new StreamWriter(manager.default_SaveFilePath + manager.icons_SaveFilePath)) {
                foreach (FileData file in manager.deskTopFileDirectory.children) {
                    if (file == null) {
                        continue;
                    }
                    sw.WriteLine("0 - " + file.name);
                }
            }
            using (StreamWriter sw = new StreamWriter(manager.default_SaveFilePath + manager.dialogue_SaveFilePath)) {
                sw.WriteLine("0");
                for (int i = 0; i < manager.totalDialogue; i++) {
                    sw.WriteLine("0 (pfb) - " + i + " (txt)");
                }
            }
            using (StreamWriter sw = new StreamWriter(manager.reset_SaveFilePath)) {
                sw.WriteLine("0 - Change this to 1 if you want to reset your save data.");
            }
            string[] eventsText = File.ReadAllLines(manager.default_SaveFilePath + manager.events_SaveFilePath);
            for (int i = 0; i < eventsText.Length; i++) {
                eventsText[i] = "0 - " + eventsText[i].Substring(4, eventsText[i].Length - 4);
            }
            File.WriteAllLines(manager.default_SaveFilePath + manager.events_SaveFilePath, eventsText);
        }
    }
}
