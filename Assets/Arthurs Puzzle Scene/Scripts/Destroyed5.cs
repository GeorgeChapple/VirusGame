using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class Destroyed5 : MonoBehaviour {
    public bool key5Destroyed = false;

    private void OnDestroy() {
        key5Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
