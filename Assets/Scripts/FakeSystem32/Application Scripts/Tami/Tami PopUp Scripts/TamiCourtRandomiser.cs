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
    public List<GameObject> allTamisSpawned = new List<GameObject>();

    public float eviiiilSpectrum = 100;
    public bool guilty;
    private void Start()
    {
        eviiiilSpectrum = Random.Range(0, 100);
        guilty = eviiiilSpectrum > 50 ? true : false;

        foreach (GameObject tami in tamiSpawnPoints)
        {
            float randNum = Random.Range(1, 3); //rand between 1 or 2
            Debug.Log(randNum);
            Vector3 randRot = new Vector3(Random.Range(-80, -100), 0, Random.Range(-80, -120)) * randNum;
            if (guilty)
            {
                if (randNum == 1)
                {
                    SpawnSad(tami, randRot);
                }
                else
                {
                    SpawnHappy(tami, randRot);
                }
            }
            else
            {
                if (randNum == 1)
                {
                    SpawnHappy(tami, randRot);
                }
                else
                {
                    SpawnSad(tami, randRot);
                }
            }
        }
    }
    private void SpawnHappy(GameObject tami, Vector3 randRot)
    {
        GameObject go = Instantiate(tamiPrefabHappy);
        go.transform.position = tami.transform.position;
        go.transform.eulerAngles = randRot;
        go.GetComponent<MeshRenderer>().material.color = tamiColours[Random.Range(0, tamiColours.Count)];
        allTamisSpawned.Add(go);
    }
    private void SpawnSad(GameObject tami, Vector3 randRot)
    {
        GameObject go = Instantiate(tamiPrefabSad);
        go.transform.position = tami.transform.position;
        go.transform.eulerAngles = randRot;
        go.GetComponent<MeshRenderer>().material.color = tamiColours[Random.Range(0, tamiColours.Count)];
        allTamisSpawned.Add(go);
    }
}
