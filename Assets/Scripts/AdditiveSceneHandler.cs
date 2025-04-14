using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class AdditiveSceneHandler : MonoBehaviour
{
    public Canvas canvas;
    public WindowSpawner manager;
    [Tooltip("Make sure the scene is available in the scene manager in build settings")]
    [SerializeField] private int sceneIndex;
    [SerializeField] private Material cameraMaterial;

    public void ButtonPress()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }
    public void SetVariables()
    {
        if (manager != null)
        {
            manager.sceneIndex = sceneIndex;
            manager.cameraMaterial = cameraMaterial;
        }
    }
    public void SetVariablesFromFileData(FileData fileData)
    {
        sceneIndex = fileData.sceneIndex;
        cameraMaterial = fileData.cameraMaterial;
    }
}
