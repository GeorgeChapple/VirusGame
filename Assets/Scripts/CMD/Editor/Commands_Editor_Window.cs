using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Commands_Editor_Window : EditorWindow
{
    private const float spacing = 20;

    private int flashAmount;
    private string searchPrompt;
    private string desktopPrompt;

    [MenuItem("Window/CMD Functions")]
    public static void ShowWindow()
    {
        GetWindow<Commands_Editor_Window>("CMD Functions");
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        flashAmount = EditorGUILayout.IntField(new GUIContent("Flash Amount", "Amount of CMD windows to flash."), flashAmount);
        if (GUILayout.Button(new GUIContent("Flash CMD", "Flashes a given amount of CMD windows."))) {
            Commands.FlashCMD(flashAmount);
        }
        GUILayout.Space(spacing);
        if (GUILayout.Button(new GUIContent("Show IP", "Opens a CMD window with the user's IP address."))) {
            Commands.ShowIP();
        }
        GUILayout.Space(spacing);
        searchPrompt = EditorGUILayout.TextField(new GUIContent("Search Prompt", "Text string to search on internet."), searchPrompt);
        if (GUILayout.Button(new GUIContent("Search Internet", "Searches the internet using the given text string."))) {
            Commands.Search(searchPrompt);
        }
        GUILayout.Space(spacing);
        desktopPrompt = EditorGUILayout.TextField(new GUIContent("Desktop File Name", "File name to create/search for on desktop."), desktopPrompt);
        if (GUILayout.Button(new GUIContent("Create Desktop File","Creates a file on desktop using given text string (if file already exists, new file will not be created)."))) {
            Commands.CreateFolderOnDesktop(desktopPrompt);
        }
        if (GUILayout.Button(new GUIContent("Search For Desktop File", "Searches for file on desktop with given text string for name."))) {
            if (Commands.CheckForFileOnDesktop(desktopPrompt)) {
                Debug.Log(desktopPrompt + " file found.");
            }
            else {
                Debug.Log(desktopPrompt + " not found.");
            }
        }
    }
}
