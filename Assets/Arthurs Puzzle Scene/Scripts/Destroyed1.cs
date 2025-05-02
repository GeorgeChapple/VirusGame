using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class Destroyed1 : MonoBehaviour {
    public bool key1Destroyed = false;

    private void OnDestroy() {
        key1Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
