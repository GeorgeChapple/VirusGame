using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class GameEventsManager : MonoBehaviour {
    [HideInInspector] public string icons_SaveFilePath = "icons.txt";

    [Tooltip("This var needs to have the desktop fileData file in it")]
    public FileData deskTopFileDirectory;

    private List<GameObject> desktopIcons = new List<GameObject>();
}
