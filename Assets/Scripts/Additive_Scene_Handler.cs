using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Additive_Scene_Handler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private WindowSpawner manager;
    [Tooltip("Make sure the scene is available in the scene manager in build settings")]
    [SerializeField] private int sceneIndex;
    [SerializeField] private Material cameraMaterial;

    public void buttonPress()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        //for (int i = 0; i < SceneManager.sceneCount; i++)
        //{
        //    if (SceneManager.GetSceneAt(i).name == scene.name)
        //    {
        //        continue;
        //    }
        //    else if (i == SceneManager.sceneCount && (SceneManager.GetSceneAt(i).name != scene.name))
        //    {
        //        SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
        //    }
        //}
    }
    public void setVariables()
    {
        if (manager != null)
        {
            manager.sceneIndex = sceneIndex;
            manager.cameraMaterial = cameraMaterial;
        }
    }
}
