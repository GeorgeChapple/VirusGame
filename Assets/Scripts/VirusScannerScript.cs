using System.Collections;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class VirusScannerScript : MonoBehaviour {
    [SerializeField] private Vector3 scannerLineStartPos;
    [SerializeField] private Vector3 scannerLineEndPos;
    [SerializeField] private bool success;
    private GameObject scannerLine;
    private SpriteHandlerScript spriteHandler;
    private bool scanning;

    private void Awake() {
        scannerLine = transform.Find("ScannerLine").gameObject;
        spriteHandler = GetComponent<SpriteHandlerScript>();
    }

    public void ScanEvent() {
        if (!scanning) {
            scanning = true;
            StartCoroutine(ScanLoop());
        }
    }

    private IEnumerator ScanLoop() {
        int animationState = 0;
        float t = 0f;
        Vector3 start = scannerLineStartPos;
        Vector3 end = scannerLineEndPos;
        Vector3 temp;
        scannerLine.SetActive(true);
        spriteHandler.spriteIndex = 3; spriteHandler.RefreshSprite();
        while (animationState < 2) {
            scannerLine.transform.localPosition = LerpV(start, end, t);
            t += Time.deltaTime;
            if (t > 1) {
                temp = end;
                end = start;
                start = temp;
                t = 0f;
                animationState++;
            }
            yield return null;
        }
        scannerLine.SetActive(false);
        if (!success) {
            StartCoroutine(FailScan());
        }
    }

    private IEnumerator FailScan() {
        int animationState = 0;
        spriteHandler.spriteIndex = 5; spriteHandler.RefreshSprite();
        while (animationState < 3) {
            yield return new WaitForSeconds(0.5f);
            spriteHandler.spriteIndex++; spriteHandler.RefreshSprite();
            yield return new WaitForSeconds(0.5f);
            spriteHandler.spriteIndex--; spriteHandler.RefreshSprite();
            animationState++;
        }
        spriteHandler.spriteIndex = 1; spriteHandler.RefreshSprite();
        scanning = false;
    }

    // REMOVE LATER, CREATE NEW SCRIPT
    // Lerp Script
    public static Vector3 LerpV(Vector3 startValue, Vector3 endValue, float t)
    {
        return (startValue + (endValue - startValue) * t);
    }
}
