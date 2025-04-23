using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class SceneSwapper : MonoBehaviour
{
    private Scene currentScene;

    private void Awake() {
        currentScene = SceneManager.GetActiveScene();
    }

    // Reloads current scene
    public void ResetScene() {
        SceneManager.LoadScene(currentScene.name);
    }

    // Load new by name
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
