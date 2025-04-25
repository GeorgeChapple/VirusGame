using System.Collections;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
*/

public class VirusScannerScript : MonoBehaviour {
    [SerializeField] private Vector3 scannerLineStartPos;
    [SerializeField] private Vector3 scannerLineEndPos;
    [SerializeField] private bool success;
    [SerializeField] private GameObject scannerLine;
    private SpriteHandlerScript spriteHandler;
    public FileData fileToScan;
    private bool scanning;

    private void Awake() {
        spriteHandler = GetComponent<SpriteHandlerScript>();
    }
    private void Start()
    {
        if (name != "Virus Scanner")
        {
            Destroy(scannerLine);
            Destroy(this);
        }
    }

    public void ScanEvent() {
        if (fileToScan == null) { Debug.Log("No file to scan"); }
        else if (fileToScan.hasVirus) { success = true; }
        if (scannerLine != null){
            if (!scanning) {
                scanning = true;
                StartCoroutine(ScanLoop());
            }
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
        if (!success){
            StartCoroutine(FailScan());
        } else { 
            StartCoroutine(SuccessScan());
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
    private IEnumerator SuccessScan() {
        int animationState = 0;
        spriteHandler.spriteIndex = 4; spriteHandler.RefreshSprite();
        while (animationState < 3) {
            yield return new WaitForSeconds(0.5f);
            spriteHandler.spriteIndex = 0; spriteHandler.RefreshSprite();
            yield return new WaitForSeconds(0.5f);
            spriteHandler.spriteIndex = 4; spriteHandler.RefreshSprite();
            animationState++;
        }
        spriteHandler.spriteIndex = 1; spriteHandler.RefreshSprite();
        scanning = false;
        success = false;
    }

    // REMOVE LATER, CREATE NEW SCRIPT
    // Lerp Script
    public static Vector3 LerpV(Vector3 startValue, Vector3 endValue, float t)
    {
        return (startValue + (endValue - startValue) * t);
    }
}
