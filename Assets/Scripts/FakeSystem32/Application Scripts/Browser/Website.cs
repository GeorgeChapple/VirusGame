using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : To house the data necessary for a website
*/

[CreateAssetMenu(fileName = "Website")]
public class Website : ScriptableObject {
    public string siteName;
    public string siteUrl;
    public GameObject websitePrefab;
    public bool active = false;

    public void Unlock() {
        FindFirstObjectByType<GameEventsManager>().NextDialogue(0);
    }
}
