using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : For tamis simple platformer to affect the tami pop up script and tami manager
*/
public class TamiPlatformerTrigger : MonoBehaviour
{
    public bool killZone;
    public TamiPopUpScript tamiPopUpScript;
    private void Awake()
    {
        tamiPopUpScript = FindAnyObjectByType<TamiPopUpScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (killZone)
        {
            other.GetComponent<TamiMovement>().Kill(); // Respawns Tami
        }
        else
        {
            tamiPopUpScript.PopUpDestroy(); // Ends platformer and does more stuff in the other scripts
        }
    }
}
