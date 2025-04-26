using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChatBoxManager))]
public class ChatBoxManager_Editor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ChatBoxManager manager = (ChatBoxManager)target;

        EditorGUILayout.LabelField(new GUIContent("Testing Reading Options", "For inspector/editor use only. Change the current file index and begin reading."));

        manager.fileIndex = Mathf.Clamp(EditorGUILayout.IntField(new GUIContent("File Index", "Index for which file in the list to read."), manager.fileIndex), 0, manager.dialogueTextFiles.Count - 1);

        if (GUILayout.Button(new GUIContent("Start Read", "Force chat box to start reading file at currently stored index."))) {
            manager.StartText(manager.fileIndex, 0);
        }
    }
}
