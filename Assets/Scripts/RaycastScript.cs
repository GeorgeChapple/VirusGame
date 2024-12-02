using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastScript : MonoBehaviour
{
    public GameObject lastHitObject;
    public RaycastHit lastHit;
    private bool leftHolding;

    private void Awake() {
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            LeftClickDown();
        }
        if (Input.GetMouseButtonUp(0)) { 
            LeftClickUp();
        }
        HitUpdate();
    }

    private void FixedUpdate() {
        lastHit = CastRaycast();
        if (!leftHolding) {
            try {
                lastHitObject = lastHit.transform.gameObject;
                Debug.Log("Hit " + lastHitObject.name);
            }
            catch {
                lastHitObject = null;
                Debug.Log("Nothing here...");
            }
        }
    }

    private RaycastHit CastRaycast() {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

    private void LeftClickDown() {
        if (lastHitObject != null && lastHitObject.GetComponent<HitEventScript>() != null && !leftHolding) {
            lastHitObject.GetComponent<HitEventScript>().hitEvent.Invoke();
        }
        leftHolding = true;
    }

    private void LeftClickUp() {
        leftHolding = false;
    }

    private void HitUpdate() {
        if (lastHitObject != null) {
            if (leftHolding) {
                if (lastHitObject.CompareTag("Window")) {
                    Vector3 distance = lastHitObject.GetComponent<WindowScript>().mouseDistance;
                    lastHitObject.transform.position = new Vector3(lastHit.point.x - distance.x, lastHit.point.y - distance.y, -0.1f);
                }
            } else {
                if (lastHitObject.CompareTag("Window")) {
                    lastHitObject.GetComponent<WindowScript>().ResetCollider();
                    lastHitObject.transform.position = new Vector3(lastHitObject.transform.position.x, lastHitObject.transform.position.y, 0.1f);
                }
            }
        }
    }
}
