using System.Collections.Generic;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : Randomises Tami's court room
*/
public class TamiCourtRandomiser : MonoBehaviour
{
    [SerializeField] private GameObject tamiPrefabHappy;
    [SerializeField] private GameObject tamiPrefabSad;

    [SerializeField] private List<Color> tamiColours = new List<Color>();
    [SerializeField] private List<GameObject> tamiSpawnPoints = new List<GameObject>();
    public List<GameObject> allTamisSpawned = new List<GameObject>();
    public List<GameObject> guiltyTamis = new List<GameObject>();
    public List<GameObject> innocentTamis = new List<GameObject>();


    public float eviiiilSpectrum = 100;
    public bool guilty;
    private void Awake()
    {
        foreach (GameObject tami in tamiSpawnPoints)
        {
            eviiiilSpectrum = Random.Range(0, 101); // Overall random num out of 100
            float randNum = Random.Range(1, 3); // Rand between 1 or 2
            Debug.Log(randNum);
            if (eviiiilSpectrum > 50) // If this tami thinks she is guilty
            {
                SpawnSad(tami, new Vector3(Random.Range(-80, -100), 0, Random.Range(-80, -120)) * randNum);
            }
            else
            {
                SpawnHappy(tami, new Vector3(Random.Range(-80, -100), 0, Random.Range(-80, -120)) * randNum);
            }
        }
        
        // The more tamis thinks she is guilty, then she is guilty and vice versa
        guilty = guiltyTamis.Count > innocentTamis.Count ? true : false;
    }
    // Spawn happy tami prefab and change its colour and such
    private void SpawnHappy(GameObject tami, Vector3 randRot)
    {
        GameObject go = Instantiate(tamiPrefabHappy);
        go.transform.position = tami.transform.position;
        go.transform.eulerAngles = randRot;
        go.GetComponent<MeshRenderer>().material.color = tamiColours[Random.Range(0, tamiColours.Count)];
        allTamisSpawned.Add(go);
        innocentTamis.Add(go);
    }
    // Vice versa
    private void SpawnSad(GameObject tami, Vector3 randRot)
    {
        GameObject go = Instantiate(tamiPrefabSad);
        go.transform.position = tami.transform.position;
        go.transform.eulerAngles = randRot;
        go.GetComponent<MeshRenderer>().material.color = tamiColours[Random.Range(0, tamiColours.Count)];
        allTamisSpawned.Add(go);
        guiltyTamis.Add(go);
    }
}
