using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
    Purpose           : To be used for the virus scanner script, shows
                        the coloured ball image for the order of platformer puzzle collection.
*/

public class VirusScannerScript : MonoBehaviour {
    [SerializeField] private Vector3 scannerLineStartPos;
    [SerializeField] private Vector3 scannerLineEndPos;
    [SerializeField] private bool success;
    [SerializeField] private GameObject scannerLine;
    [SerializeField] private Image colouredBallImage;
    private SpriteHandlerScript spriteHandler;
    public FileData fileToScan;
    private bool scanning;

    private void Awake() {
        spriteHandler = GetComponent<SpriteHandlerScript>();
        colouredBallImage.enabled = false;
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
        else if (fileToScan.hasVirus) { success = true; } // Checks if file "has virus" and sets success to true for it - Jason
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
            // Starts success scan after some variables set - Jason
            colouredBallImage.enabled = true;
            colouredBallImage.sprite = fileToScan.colouredBallForVirusScanner;
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
        success = false;
    }
    // Just like fail scan but with different sprite indexes - Jason
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
        colouredBallImage.enabled = false;
        fileToScan = null; 
    }

    // REMOVE LATER, CREATE NEW SCRIPT
    // Lerp Script
    public static Vector3 LerpV(Vector3 startValue, Vector3 endValue, float t)
    {
        return (startValue + (endValue - startValue) * t);
    }
}
