using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowContent : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;
    [SerializeField] private SpriteRenderer content;
    [SerializeField] private Material cameraMaterial;
    void Start()
    {
        bool shouldSwap = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == scene.name) { shouldSwap = true; }
        }
        if (shouldSwap)
        {
            content.material = cameraMaterial;
        }
    }

}
