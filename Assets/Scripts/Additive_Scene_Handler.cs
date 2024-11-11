using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Additive_Scene_Handler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void buttonPress()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
        //canvas.enabled = false;
    }
}
