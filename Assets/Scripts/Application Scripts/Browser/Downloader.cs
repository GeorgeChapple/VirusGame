using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
