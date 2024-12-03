using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefeb;
    [SerializeField] private Color[] colours;

    [Header("Spawning Variables")]
    public SceneAsset scene;
    public SpriteRenderer content;
    public Material cameraMaterial;

    public void SpawnWindow() {
        if (scene != null && content != null && cameraMaterial != null)
        {
            
        }
        GameObject obj = Instantiate(windowPrefeb, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), transform.rotation);
        obj.GetComponent<Renderer>().material.color = colours[Random.Range(0, colours.Length)];
    }
}
