using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downloader : MonoBehaviour
{
    public GenericEventHandler eventToCall;
    public FileData fileToDownload;
    public void DownloadFile()
    {
        if (fileToDownload != null && eventToCall != null)
        {
            fileToDownload.isAvailable = true;
            eventToCall.ByScriptEvent.Invoke();
        }
    }
}
