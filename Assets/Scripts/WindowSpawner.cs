using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UnityEngine.GameObject windowPrefab;
    [SerializeField] private Color[] colours;

    [Header("Spawning Variables")]
    public int sceneIndex;
    public Material cameraMaterial;

    public void SpawnWindow() {
        UnityEngine.GameObject obj = Instantiate(windowPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), transform.rotation, canvas.transform);
        obj.GetComponent<Image>().material.color = colours[Random.Range(0, colours.Length)];
        if (sceneIndex >= 0 && cameraMaterial != null)
        {
            obj.GetComponent<WindowContent>().SetManager(this);
            obj.GetComponent<WindowContent>().OnceSpawned();
        }
        sceneIndex = -1;
        cameraMaterial = null;
    }
}
