using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : For use with applications that
                        load scenes additively, this should've
                        been done with a script in the actual
                        scene like tami, but i dont have much time
*/
public class WindowContent : MonoBehaviour
{
    private FakeWindows32 windows32;
    [SerializeField] private WindowSpawner manager;
    [SerializeField] private string sceneName;
    [SerializeField] private Image content;
    [SerializeField] private Material cameraMaterial;
    private void Awake()
    {
        windows32 = FindAnyObjectByType<FakeWindows32>();
        windows32.OnHierarchyUpdated();
        gameObject.name = "Window " + Random.Range(0, 99);
    }

    public void OnceSpawned()
    {
        if (manager != null)
        {
            sceneName = manager.sceneName;
            cameraMaterial = manager.cameraMaterial;
            content.material = cameraMaterial;
        }        
    }

    public void SetManager(WindowSpawner windowSpawner)
    {
        manager = windowSpawner;
    }

    public void DestroyConnectedScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
