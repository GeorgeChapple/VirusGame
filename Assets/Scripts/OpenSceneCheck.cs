using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneCheck : MonoBehaviour {
    [SerializeField] private string generatingScene = "GeneratingNewPuzzles";

    void Start() {
        GameEventsManager gem = GetComponent<GameEventsManager>();
        if (gem != null && gem.dialogueIndex == 8) {
            GetComponent<VideoLoadScene>().sceneName = generatingScene;
        }
    }
}
