using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To load a scene after a video plays
*/
public class VideoLoadScene : MonoBehaviour
{
    VideoPlayer player;
    public string sceneName;

    private void Awake()
    {
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
