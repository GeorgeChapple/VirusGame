using UnityEngine;

public class SpawnDaisyFolder : MonoBehaviour { 
    void Start() {
        Commands.CreateFolderOnDesktop(GetComponent<GameEventsManager>().daisyFileName);
    }
}
