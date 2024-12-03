using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefeb;
    [SerializeField] private Color[] colours;

    [Header("Spawning Variables")]
    public int sceneIndex;
    public Material[] cameraMaterial;

    public void SpawnWindow() {
        GameObject obj = Instantiate(windowPrefeb, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), transform.rotation);
        obj.GetComponent<Renderer>().material.color = colours[Random.Range(0, colours.Length)];
        if (sceneIndex >= 0 && cameraMaterial != null)
        {
            obj.GetComponent<WindowContent>().SetManager(this);
            obj.GetComponent<WindowContent>().OnceSpawned();
        }
        sceneIndex = -1;
        cameraMaterial = null;
    }
}
