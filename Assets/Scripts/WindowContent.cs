using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowContent : MonoBehaviour
{
    [SerializeField] private WindowSpawner manager;
    [SerializeField] private int sceneIndex;
    [SerializeField] private Image content;
    [SerializeField] private Material cameraMaterial;

    public void OnceSpawned()
    {
        //manager = FindAnyObjectByType<WindowSpawner>();
        if (manager != null)
        {
            sceneIndex = manager.sceneIndex;
            cameraMaterial = manager.cameraMaterial;
            content.material = cameraMaterial;
        }

        //bool shouldSwap = false;
        //for (int i = 0; i < SceneManager.sceneCount; i++)
        //{
        //    if (SceneManager.GetSceneAt(i).name == scene.name) { shouldSwap = true; }
        //}
        //if (shouldSwap)
        //{
        //    cameraMaterial = manager.cameraMaterial;
        //    content.material = cameraMaterial;
        //}
    }

    public void SetManager(WindowSpawner windowSpawner)
    {
        manager = windowSpawner;
    }

    public void DestroyConnectedScene()
    {
        if (sceneIndex >= 0)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}
