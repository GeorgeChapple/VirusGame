using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
//[Serializable]
public class FileType : MonoBehaviour
{
    public bool root = false;
    public Sprite icon;
    public string fileName = "";
    public string dataType = "";
    public int filePointer = 0;
    public List<FileType> children = new List<FileType>();
    public FileType parent;

    public void SetUp()
    {
        if (fileName == "Root")
        {
            root = true;
        }
    }
    public void AddToChildren(FileType file)
    {
        children.Add(file);
    }
}
