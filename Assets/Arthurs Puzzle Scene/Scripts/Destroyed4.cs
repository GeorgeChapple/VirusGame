using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class Destroyed4 : MonoBehaviour {
    public bool key4Destroyed = false;

    private void OnDestroy() {
        key4Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
