using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/

[CreateAssetMenu(fileName = "Website")]
public class Website : ScriptableObject
{
    public string siteName;
    public string siteUrl;
    public GameObject websitePrefab;
    public bool active = false;

    public void Unlock()
    {
        FindFirstObjectByType<GameEventsManager>().NextDialogue(0);
    }
}
