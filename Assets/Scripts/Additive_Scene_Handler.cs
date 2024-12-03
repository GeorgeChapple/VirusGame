using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.UI;
using UnityEditor;

public class Additive_Scene_Handler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private WindowSpawner manager;
    [SerializeField] private SceneAsset scene;
    [SerializeField] private SpriteRenderer content;
    [SerializeField] private Material cameraMaterial;
    public void buttonPress()
    {
        SceneManager.LoadScene(scene.name,LoadSceneMode.Additive);
        //canvas.enabled = false;
    }
    public void setVariables()
    {
        if (manager != null)
        {
            manager.scene = scene;
            manager.cameraMaterial = cameraMaterial;
            manager.content = content;
        }
    }
}
