using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : Load scenes additively for use with fake applications
*/

public class AdditiveSceneHandler : MonoBehaviour
{
    public Canvas canvas;
    public WindowSpawner manager;
    [Tooltip("Make sure the scene is available in the scene manager in build settings")]
    [SerializeField] private string sceneName;
    [SerializeField] private Material cameraMaterial;
    [SerializeField] private WindowsButton windowsButton;
    // Loads scene
    public void ButtonPress()
    {
        if (windowsButton.application != null)
        {
            return;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    // Sets scene name and camera mat
    public void SetVariables()
    {
        if (windowsButton.application != null)
        {
            return;
        }
        if (manager != null)
        {
            manager.sceneName = sceneName;
            manager.cameraMaterial = cameraMaterial;
        }
    }
    public void SetVariablesFromFileData(FileData fileData)
    {
        sceneName = fileData.sceneName;
        cameraMaterial = fileData.cameraMaterial;
    }
}