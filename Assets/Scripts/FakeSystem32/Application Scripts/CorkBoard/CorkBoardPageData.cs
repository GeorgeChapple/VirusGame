using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : House all data for corkboard page
*/
[CreateAssetMenu(fileName = "Cork Board Data")]
public class CorkBoardPageData : ScriptableObject
{
    public string Text;
    public bool active = true;

    public void Unlock()
    {
        active = true;
    }
}