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
            if (sceneName == "UI_Test 1") {
                GameEventsManager gem = FindFirstObjectByType<GameEventsManager>();
                if (gem != null) {
                    gem.NextDialogue(10, false, true);
                }
            }
            sceneSwapper.ChangeScene(sceneName);
        }
    }
}