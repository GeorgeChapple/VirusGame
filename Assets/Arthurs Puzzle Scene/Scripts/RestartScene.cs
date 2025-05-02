using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class RestartScene : MonoBehaviour {
    public void restartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
