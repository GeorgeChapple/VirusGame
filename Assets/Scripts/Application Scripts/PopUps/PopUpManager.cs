using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject hierarchy;
    [SerializeField] private float popUpSpawnInterval;
    [SerializeField] private bool spawnPopUps;

    private void Start()
    {
        spawnPopUps = true;
        StartCoroutine(popUpSpawnLoop());
    }
    private IEnumerator popUpSpawnLoop()
    {
        while (spawnPopUps)
        {            
            yield return new WaitForSeconds(popUpSpawnInterval); 
            Instantiate(popUpPrefab, hierarchy.transform);
        }
    }
}
