using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefeb;
    [SerializeField] private Color[] colours;

    public void SpawnWindow() {
        GameObject obj = Instantiate(windowPrefeb, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), transform.rotation);
        obj.GetComponent<Renderer>().material.color = colours[Random.Range(0, colours.Length)];
    }
}
