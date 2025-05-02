using UnityEngine;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman, George Chapple
*/

// When I have time I will re-write the lot of these scripts for Arthur - George

public class OrderManager : MonoBehaviour {
    [SerializeField] Destroyed1 Test;
    [SerializeField] Destroyed2 Test2;
    [SerializeField] Destroyed3 Test3;
    [SerializeField] Destroyed4 Test4;
    [SerializeField] Destroyed5 Test5;
    private bool oneCollect = false;
    private bool twoCollect = false;
    private bool threeCollect = false;
    private bool fourCollect = false;
    private bool fiveCollect = false;
    private bool finished = false;

    // Update is called once per frame
    void Update() { //For order of the keys - check what is grabbed
        if (!finished) {
            if (Test == null && Test2 != null && Test3 != null && Test4 != null && Test5 != null) {
                oneCollect = true;
                Debug.Log("1");
            }
            if (oneCollect == true && Test2 == null && Test3 != null && Test4 != null && Test5 != null) {
                twoCollect = true;
                Debug.Log("2");
            }
            if (twoCollect == true && Test3 == null && Test4 != null && Test5 != null) {
                threeCollect = true;
                Debug.Log("3");
            }
            if (threeCollect == true && Test4 == null && Test5 != null) {
                fourCollect = true;
                Debug.Log("4");
            }
            if (fourCollect == true && Test5 == null) {
                fiveCollect = true;
                Debug.Log("5");
            }
            if (fiveCollect == true) {
                print("ORDER CORRECT");
                PlayerPrefs.SetInt("PlatformPuzzleWin", 2);
                finished = true;
            } else if (Test == null && Test2 == null && Test3 == null && Test4 == null && Test5 == null) {
                PlayerPrefs.SetInt("PlatformPuzzleWin", 1);
                finished = true;
            }
        }
    }
}