using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastScript : MonoBehaviour
{
    public UnityEngine.GameObject lastHitObject;
    public RaycastHit lastHit;
    private bool leftHolding;
    [SerializeField] private Canvas canvas;

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
                    //lastHitObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x - (canvas.pixelRect.width / 2), Input.mousePosition.y - (canvas.pixelRect.height / 2), -0.1f) - distance;
                    lastHitObject.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.1f) - distance;
                }
            } else {
                if (lastHitObject.CompareTag("Window")) {
                    lastHitObject.GetComponent<WindowScript>().ResetCollider();
                    lastHitObject.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new Vector3(lastHitObject.GetComponent<WindowScript>().GetRectTransform().anchoredPosition.x, lastHitObject.GetComponent<WindowScript>().GetRectTransform().anchoredPosition.y, 0.1f);
                }
            }
        }
    }
}
