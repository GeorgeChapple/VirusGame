using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
[CreateAssetMenu(fileName = "File", menuName = "Psuedo File Directory")]
public class FileData : ScriptableObject
{
    [Header("Variables")]
    public bool root = false;
    [Tooltip("These are the sprites that will go into the SpriteHandlerScript")]
    public List<Sprite> icon;
    public new string name = "";
    public string dataType = "";

    [Header("Hierarchy")]
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public List<FileData> linkedFiles = new List<FileData>();
    public FileExplorer fileExplorer;
    public GameObject selfBackUp;
    public GameObject self;

    [Header("Application")]
    [Tooltip("The Application must be either the Windows base Icon or Windows Taskbar base Icon")]
    public GameObject application;
    public UnityEvent OnSpawnAsDeskTopIcon;
    public UnityEvent OnSpawnAsTaskBarIcon;
    public UnityEvent OnSpawnAsFileExplorerIcon;
    public UnityEvent OnDoubleClick;

    [Header("Additive Scene Handler")]
    public int sceneIndex;
    public Material cameraMaterial;


    public static FileDataObject Convert(FileData fd)
    {
        fd = CreateInstance<FileData>();
        FileDataObject fdo = new FileDataObject();
        fdo.root = fd.root;
        fdo.icon = fd.icon;
        fdo.name = fd.name;
        fdo.dataType = fd.dataType;
        fdo.children = fd.children;
        fdo.parent = fd.parent;
        fdo.fileExplorer = fd.fileExplorer;
        fdo.application = fd.application;
        return fdo;
    }

}

public class FileDataObject : MonoBehaviour
{
    [Header("Variables")]
    public bool root = false;
    public List<Sprite> icon;
    public new string name = "";
    public string dataType = "";

    [Header("Hierarchy")]
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public List<FileData> linkedFiles = new List<FileData>();
    public FileExplorer fileExplorer;

    [Header("Application")]
    [Tooltip("The Application must be either the Windows base Icon or Windows Taskbar base Icon")]
    public GameObject application;
    public UnityEvent OnSpawnAsDeskTopIcon;
    public UnityEvent OnSpawnAsTaskBarIcon;
    public UnityEvent OnSpawnAsFileExplorerIcon;
    public UnityEvent OnDoubleClick;
    public static FileData Convert(FileDataObject fdo)
    {
        FileData fd = ScriptableObject.CreateInstance<FileData>();
        fd.root = fdo.root;
        fd.icon = fdo.icon;
        fd.name = fdo.name;
        fd.dataType = fdo.dataType;
        fd.children = fdo.children;
        fd.parent = fdo.parent;
        fd.fileExplorer = fdo.fileExplorer;
        fd.application = fdo.application;
        return fd;
    }
}
