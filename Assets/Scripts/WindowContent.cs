using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class WindowContent : MonoBehaviour
{
    private FakeWindows32 windows32;
    [SerializeField] private WindowSpawner manager;
    [SerializeField] private int sceneIndex;
    [SerializeField] private Image content;
    [SerializeField] private Material cameraMaterial;
    private void Awake()
    {
        windows32 = GameObject.FindAnyObjectByType<FakeWindows32>();
        //windows32.windowHierarchy.Insert(0, GetComponent<WindowScript>());
        windows32.OnHierarchyUpdated();
        gameObject.name = "Window " + Random.Range(0, 99);
    }
    public void OnceSpawned()
    {
        if (manager != null)
        {
            sceneIndex = manager.sceneIndex;
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
        if (sceneIndex >= 0)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}
