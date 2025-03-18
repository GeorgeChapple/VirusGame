using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UnityEngine.GameObject windowPrefab;
    [SerializeField] private Color[] colours;

    [Header("Spawning Variables")]
    public int sceneIndex;
    public Material cameraMaterial;

    public void SpawnWindow() {
        UnityEngine.GameObject obj = Instantiate(windowPrefab, new Vector3(0, 0, 0), transform.rotation, canvas.transform);
        if (sceneIndex >= 0 && cameraMaterial != null) {
            obj.GetComponent<WindowContent>().SetManager(this);
            obj.GetComponent<WindowContent>().OnceSpawned();
        }
        sceneIndex = -1;
        cameraMaterial = null;
    }
}
