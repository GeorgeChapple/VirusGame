using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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