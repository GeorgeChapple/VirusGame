using System.IO;
using UnityEditor;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class FileExplorer : MonoBehaviour
{
    //so this will quite literally be a file explorer
    //now I would like to make it as proper as possible like having the desktop folder show what is in there
    //but since that would probably mean I'd have to remake
    //how the desktop icons work and stuff with making structs for types of data(applications, images, etc.)
    //ofc the only reason to do that would make it easier to add to and more modular
    //but we don't have a lot of time so i'm just gonna make it work continuing on with how ive been doing it
    [SerializeField] private TextAsset file;
    private string filePath;
    private string errorMessage = "***Syntax Error***";
    public bool autoLineBreak = true;

    public bool forceSkip;

    void Awake()
    {
        filePath = AssetDatabase.GetAssetPath(file);
    }
    public void StartRead()
    {
        ReadPathsAndSetUp();
    }
    private void ReadPathsAndSetUp()
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            //for loop here
            //this needs to be instantly
        }
    }
}

