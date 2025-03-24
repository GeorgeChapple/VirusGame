using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FileData : MonoBehaviour
{
    public bool root = false;
    public Sprite icon;
    public string fileName = "";
    public string dataType = "";
    public List<FileData> children = new List<FileData>();
    public FileData parent;
    public FileExplorer fileExplorer;
    public GameObject application;
    
    public void PassThrough()
    {
        fileExplorer.ChangeCurrentFolder(this);
    }
}

//public class FileData : ScriptableObject
//{
//    public bool root = false;
//    public Sprite icon;
//    public string fileName = "";
//    public string dataType = "";
//    public List<FileData> children = new List<FileData>();
//    public FileData parent;
//    public GameObject application;
//}
