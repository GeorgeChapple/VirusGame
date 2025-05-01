using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : Sets the file to be spawnable by the filing system,
                        directly changes a scriptable objects data (doesn't save)
*/
public class Downloader : MonoBehaviour
{
    public FileData fileToDownload;
    public void DownloadFile()
    {
        if (fileToDownload != null)
        {
            fileToDownload.isAvailable = true;
        }
    }
}
