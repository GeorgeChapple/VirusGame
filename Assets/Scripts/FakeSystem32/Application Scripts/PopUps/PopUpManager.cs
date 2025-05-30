using System.Collections;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To spawn Pop-Ups at a set spawn rate
*/
public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject hierarchy;
    [SerializeField] private float popUpSpawnInterval;
    public bool spawnPopUps;

    // Spawns a new popup after set seconds
    private IEnumerator popUpSpawnLoop()
    {
        while (spawnPopUps)
        {            
            yield return new WaitForSeconds(popUpSpawnInterval); 
            Instantiate(popUpPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity, hierarchy.transform);
        }
    }
    public void StartSpawningPopUps() 
    {
        spawnPopUps = true;
        StartCoroutine(popUpSpawnLoop());
    }
    public void StopSpawningPopUps()
    {
        spawnPopUps = false;
    }
}
