using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
[CreateAssetMenu(fileName = "File", menuName = "Psuedo File Directory")]
public class FileData : ScriptableObject
{
    public bool root = false;
    public Sprite icon;
    public new string name = "";
    public string dataType = "";
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public List<FileData> linkedFiles = new List<FileData>();
    public FileExplorer fileExplorer;
    public GameObject application;
    
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
    public bool root = false;
    public Sprite icon;
    public new string name = "";
    public string dataType = "";
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public FileExplorer fileExplorer;
    public GameObject application;
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
    public void PassThrough()
    {
        //fileExplorer.ChangeCurrentFolder(this);
    }
}
