using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class EndGameSceneTriggerScript : MonoBehaviour {
    [SerializeField]
    [Tooltip("Name of scene to load next")]
    private string sceneName;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.TryGetComponent<SceneSwapper>(out SceneSwapper sceneSwapper)) {
            sceneSwapper.ChangeScene(sceneName);
        }
    }
}