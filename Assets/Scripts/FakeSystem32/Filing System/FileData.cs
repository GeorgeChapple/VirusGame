using System;
using System.Collections.Generic;
using UnityEngine;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : Houses all data about a file, for use
                        with the filing system to create icon
                        buttons for applications and such.
*/

[CreateAssetMenu(fileName = "File")]
public class FileData : ScriptableObject
{
    [Header("Variables")]
    public bool root = false;
    [Tooltip("These are the sprites that will go into the SpriteHandlerScript")]
    public List<Sprite> icon;
    public new string name = "";
    public bool hasVirus = false;
    public enum DataType { Folder=0, Application=1, Image=2, File=3 };
    public DataType dataType;

    [Header("Hierarchy")]
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public List<FileData> linkedFiles = new List<FileData>();
    public FileExplorer fileExplorer;

    [Header("Application")]
    [Tooltip("The Application must be either the window prefab or an application prefab like file explorer")]
    public GameObject application;
    public List<EventPass> OnDoubleClick;

    [Header("Additive Scene Handler")]
    public string sceneName;
    public Material cameraMaterial;

    [Header("Miscellaneous")]
    public bool canBeTaskBarIcon = true;
    public bool canBeDragged = true;
    public bool isAvailable = true;
    public Sprite colouredBallForVirusScanner;
}

// EventPass was the solution I came up with to the fact that
// the clicks used events for their function. I tried using Unity
// Events but unfortunately unity doesnt have support for changing a
// variable on an event in code and can only be changed in editor.
// Since that was the case I realised that there wasnt an easy way
// and that I'd have to rebuild the unity event from scratch instead.

// This just makes it easier, it used to be its own script with a CreateAssetMenu
[Serializable] 
public class EventPass
{
    public bool passValThrough;
    public string methodName;
    
    public bool passSelfVal;
    public FileData self;

    public bool passStringVal;
    public string stringVal;

    public bool passIntVal;
    public int intVal;

    public bool passFloatVal;
    public float floatVal;

    public bool passBoolVal;
    public bool boolVal;
}