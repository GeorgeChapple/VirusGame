using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class MovingKillZonesManagerScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Moving kill zones will lerp to the position of this gameobject.")]
    private GameObject endPositionGameObject;
    [SerializeField]
    [Tooltip("Time kill zones will take to move to new end position")]
    private float lerpTime;
    private Vector3 endPosition;
    private LerpScript lerpScript;

    private void Awake() {
        lerpScript = GetComponent<LerpScript>();
        endPosition = endPositionGameObject.transform.position;
    }

    private void Start() {
        lerpScript.StartVector3Lerp(transform.position, endPosition, lerpTime);
    }

    private void Update() {
        transform.position = lerpScript.lerpVector;
    }
}
