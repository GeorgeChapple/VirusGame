using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class Destroyed2 : MonoBehaviour {
    public bool key2Destroyed = false;

    private void OnDestroy()
    {
        key2Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
