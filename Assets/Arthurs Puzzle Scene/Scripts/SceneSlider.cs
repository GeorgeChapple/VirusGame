using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class SceneSlider : MonoBehaviour {
    [SerializeField] Transform wholeScene;
    private void Awake() {
        //move the thingmabob
        wholeScene.position = new Vector3(Random.Range(50, 200), 0);
    }
}
