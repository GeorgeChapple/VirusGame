using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
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
            other.GetComponent<TamiMovement>().Kill();
        }
        else
        {
            tamiPopUpScript.PopUpDestroy();
        }
    }
}
