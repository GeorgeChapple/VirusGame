using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoLoadScene : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField] private string sceneName;
    private void Awake()
    {
        //force resolution 1920x1080 to keep canvas spacing(i dont really want to but i dont know how to scale all of this properly, maybe this will be for later)
        Screen.SetResolution(1920, 1080, true);
        player = GetComponent<VideoPlayer>();
        StartCoroutine(enumerator());
    }
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds((float)player.length);
        SceneManager.LoadScene(sceneName);
    }

}
