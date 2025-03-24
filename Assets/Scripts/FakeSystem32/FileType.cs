using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
[Serializable]
public class FileType
{
    public bool root = false;
    public Sprite icon;
    public string fileName = "";
    public string dataType = "";
    public int filePointer = 0;
    public List<FileType> children = new List<FileType>();
    public FileType parent;
    public FileExplorerOld fileExplorer;

    public void AddToChildren(FileType file)
    {
        children.Add(file);
    }
    public FileType Convert(FileTypeObject fto)
    {
        FileType ft = new FileType();

        ft.root = fto.root;
        ft.icon = fto.icon;
        ft.fileName = fto.fileName;
        ft.dataType = fto.dataType;
        ft.filePointer = fto.filePointer;
        ft.children = fto.children;
        ft.parent = fto.parent;
        ft.fileExplorer = fto.fileExplorer;

        return ft;
    }

}
public class FileTypeObject : MonoBehaviour
{
    public bool root = false;
    public Sprite icon;
    public string fileName = "";
    public string dataType = "";
    public int filePointer = 0;
    public List<FileType> children = new List<FileType>();
    public FileType parent;
    public FileExplorerOld fileExplorer;
    public void ReceiveFolder()
    {
        fileExplorer.Retreive(this);
    }

    public FileTypeObject Convert(FileType ft)
    {
        FileTypeObject fto = new FileTypeObject();

        fto.root = ft.root;
        fto.icon = ft.icon;
        fto.fileName = ft.fileName;
        fto.dataType = ft.dataType;
        fto.filePointer = ft.filePointer;
        fto.children = ft.children;
        fto.parent = ft.parent;
        fto.fileExplorer = ft.fileExplorer;

        return fto;
    }
}