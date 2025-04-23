using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class KillZoneScript : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.TryGetComponent<DeathScript>(out DeathScript deathScript)) {
            deathScript.KillPlayer();
        }
    }
}
