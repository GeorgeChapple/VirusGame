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

        if (GUILayout.Button(new GUIContent("Set Save Files", "Sets up save data files/sets save data files to default values."))) {
            using (StreamWriter sw = new StreamWriter(manager.icons_SaveFilePath)) {
                foreach (FileData file in manager.deskTopFileDirectory.children) {
                    if (file == null) {
                        continue;
                    }
                    sw.WriteLine("0 - " + file.name);
                }
            }
        }
    }
}
