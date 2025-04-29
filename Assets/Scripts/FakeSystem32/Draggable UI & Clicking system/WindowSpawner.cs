using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
*/

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject hierarchy;
    [SerializeField] private Color[] colours;

    [Header("Spawning Variables")]
    public string sceneName;
    public Material cameraMaterial;

    public GameObject SpawnWindow(GameObject windowPrefab)
    {
        UnityEngine.GameObject obj = Instantiate(windowPrefab, new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Quaternion.identity, hierarchy.transform);
        if (sceneName != null && cameraMaterial != null)
        {
            obj.GetComponent<WindowContent>().SetManager(this);
            obj.GetComponent<WindowContent>().OnceSpawned();
        }
        sceneName = null;
        cameraMaterial = null;
        return obj;
    }
}