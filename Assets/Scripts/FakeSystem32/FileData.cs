using System.Collections.Generic;
using UnityEngine;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
*/

[CreateAssetMenu(fileName = "File")]
public class FileData : ScriptableObject
{
    [Header("Variables")]
    public bool root = false;
    [Tooltip("These are the sprites that will go into the SpriteHandlerScript")]
    public List<Sprite> icon;
    public new string name = "";
    //public string dataType = "";
    public enum DataType { Folder=0, Application=1 };
    public DataType dataType;

    [Header("Hierarchy")]
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public List<FileData> linkedFiles = new List<FileData>();
    public FileExplorer fileExplorer;

    [Header("Application")]
    [Tooltip("The Application must be either the window prefab or an application prefab like file explorer")]
    public GameObject application;
    public List<EventPass> OnSpawnAsDeskTopIcon;
    public List<EventPass> OnSpawnAsTaskBarIcon;
    public List<EventPass> OnSpawnAsFileExplorerIcon;
    public List<EventPass> OnDoubleClick;

    [Header("Additive Scene Handler")]
    public string sceneName;
    public Material cameraMaterial;

    [Header("Miscellaneous")]
    public bool canBeTaskBarIcon = true;
    public bool canBeDragged = true;
}
