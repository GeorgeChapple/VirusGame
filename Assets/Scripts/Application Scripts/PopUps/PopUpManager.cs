using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject hierarchy;
    [SerializeField] private float popUpSpawnInterval;
    [SerializeField] private bool spawnPopUps;

    //delete this when jimbo finishes game events
    private void Start()
    {
        StartSpawningPopUps();
    }
    private IEnumerator popUpSpawnLoop()
    {
        while (spawnPopUps)
        {            
            yield return new WaitForSeconds(popUpSpawnInterval); 
            Instantiate(popUpPrefab, hierarchy.transform);
        }
    }
    public void StartSpawningPopUps() 
    {
        spawnPopUps = true;
        StartCoroutine(popUpSpawnLoop());
    }
}
