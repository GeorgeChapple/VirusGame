using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
    Purpose           : To shoot a raycast from the camera
                        and get a certain then move it
                        to mouse position if
                        conditions are met
*/

public class RaycastScript : MonoBehaviour {
    [SerializeField] private Canvas canvas;
    public UnityEngine.GameObject lastHitObject;
    public RaycastHit lastHit;
    private SoundScript soundScript;
    private bool leftHolding;
    private int soundIndex;

    private void Awake() {
        soundScript = GetComponent<SoundScript>();
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
            }
            catch {
                lastHitObject = null;
            }
        }
    }

    private RaycastHit CastRaycast() {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

    private void LeftClickDown() {
        if (soundIndex > soundScript.sounds.Length - 1) {
            soundIndex = 0;
        }
        soundScript.PlaySound(soundIndex, 1, 1);
        soundIndex++;
        if (lastHitObject != null && lastHitObject.GetComponent<HitEventScript>() != null && !leftHolding) {
            lastHitObject.GetComponent<HitEventScript>().hitEvent.Invoke();
        }
        leftHolding = true;
    }

    private void LeftClickUp() {
        if (lastHitObject != null && lastHitObject.GetComponent<HitEventScript>() != null && leftHolding)
        {
            lastHitObject.GetComponent<HitEventScript>().letGoEvent.Invoke();
        }
        leftHolding = false;
    }

    private void HitUpdate() {
        if (lastHitObject != null) {
            if (leftHolding) {
                if (lastHitObject.CompareTag("Draggable")) {
                    Vector3 distance = lastHitObject.GetComponent<WindowScript>().mouseDistance;

                    // Use mouse position for object tracking, this means it can be used on ui objects. - Jason
                    lastHitObject.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.1f) - distance; 
                }
            } else {
                if (lastHitObject.CompareTag("Draggable")) {
                    lastHitObject.GetComponent<WindowScript>().ResetCollider();
                    lastHitObject.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new Vector3(lastHitObject.GetComponent<WindowScript>().GetRectTransform().anchoredPosition.x, lastHitObject.GetComponent<WindowScript>().GetRectTransform().anchoredPosition.y, 0.1f);
                }
            }
        }
    }
}
