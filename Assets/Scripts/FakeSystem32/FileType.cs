using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class FileType
{
    public bool root;
    public Sprite icon;
    public string name;
    public string fileType;
    public List<FileType> children;
    public FileType parent;
}
