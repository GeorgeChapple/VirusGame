using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class Destroyed3 : MonoBehaviour {
    public bool key3Destroyed = false;

    private void OnDestroy() {
        key3Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
