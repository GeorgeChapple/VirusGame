using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class TamiCourtRandomiser : MonoBehaviour
{
    [SerializeField] private GameObject tamiPrefabHappy;
    [SerializeField] private GameObject tamiPrefabSad;

    [SerializeField] private List<Color> tamiColours = new List<Color>();
    [SerializeField] private List<GameObject> tamiSpawnPoints = new List<GameObject>();

    private float eviiiilSpectrum = 100;
    private void Start()
    {
        eviiiilSpectrum = Random.Range(0, 100);

        foreach (GameObject tami in tamiSpawnPoints)
        {
            float rand = Random.Range(0, 100);
            if (eviiiilSpectrum > 50)
            {
                //guilty
                
            }
            else
            {
                //innocent
            }
        }
    }

}
