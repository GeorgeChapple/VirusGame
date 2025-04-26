using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoLoadScene : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField] private string sceneName;
    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        StartCoroutine(enumerator());
    }
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds((float)player.length);
        SceneManager.LoadScene(sceneName);
    }

}
