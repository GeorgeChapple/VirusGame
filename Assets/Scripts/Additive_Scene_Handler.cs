using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Additive_Scene_Handler : MonoBehaviour
{
    public void buttonPress()
    {
        print("trying to add scene");
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
    }
}
