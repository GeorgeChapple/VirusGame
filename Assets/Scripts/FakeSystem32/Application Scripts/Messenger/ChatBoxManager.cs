using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class ChatBoxManager : MonoBehaviour
{
    [HideInInspector] public int fileIndex = 0;
    [HideInInspector] public int currentPrefabIndex = 0;
    [HideInInspector] public bool finished = false;

    public List<TextAsset> dialogueTextFiles = new List<TextAsset>();
    public List<TextAsset> daisyTextFiles = new List<TextAsset>();

    private SpeechManager speechManager; // Stores the speech manager script attatched to the chatbox
    private RectTransform contentRect; // Stores the content UI, this is where the speech bubbles get placed on to be scrolled through
    private GridLayoutGroup contentRectGridBox;
    private GameEventsManager gameEventsManager;
    private GameObject closeBlocker;
    private GameObject desktopIcon;
    private Transform daisyTeleportPoint;
    private DaisyScript daisy;
    private SoundScript soundScript;
    private int daisySpriteIndex = 0;
    private const string daisyAnims_FilePath= "currentAnim.txt";
    private string[] daisyAnims;

    private void Awake() {
        desktopIcon = GameObject.Find("Email");
        soundScript = GetComponent<SoundScript>();
        daisyTeleportPoint = GameObject.Find("DaisyPoint").transform;
        gameEventsManager = FindFirstObjectByType<GameEventsManager>();
        daisy = FindFirstObjectByType<DaisyScript>();
        if (desktopIcon != null ) {
            desktopIcon.GetComponent<SpriteHandlerScript>().spriteIndex = 1;
            desktopIcon.GetComponent<SpriteHandlerScript>().RefreshSprite();
        }
        closeBlocker = transform.GetChild(3).gameObject;
        contentRect = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        contentRectGridBox = contentRect.GetComponent<GridLayoutGroup>();
        speechManager = GetComponent<SpeechManager>();
        speechManager.newBubbleCreated.AddListener(AddSpeechBubble);
    }

    private void Start() {
        soundScript.PlaySound(0, 1, 1);
        StartText(gameEventsManager.dialogueIndex, gameEventsManager.dialoguePrefabIndex);
    }

    private void OnDestroy() {
        if (desktopIcon != null) {
            desktopIcon.GetComponent<SpriteHandlerScript>().spriteIndex = 0;
            desktopIcon.GetComponent<SpriteHandlerScript>().RefreshSprite();
        }
        if (gameEventsManager != null) {
            gameEventsManager.soundScript.PlaySound(1, 1, 1);
            gameEventsManager.textReadOnce = false;
        }
        if (daisy != null) {
            daisy.chatBoxActive = false;
            daisy.UpdateAnimator("IDLE");
        }
    }

    // If not already reading, read file at given index
    public void StartText(int index, int prefabIndex) {
        if (daisy != null) {
            daisySpriteIndex = 0;
            daisy.chatBoxActive = true;
            daisy.UpdateAnimator("IDLE");
        }
        using (StreamWriter sw = new StreamWriter(daisyAnims_FilePath)) {
            sw.Write(daisyTextFiles[gameEventsManager.dialogueIndex].text);
        }
        daisyAnims = File.ReadAllLines(daisyAnims_FilePath);
        finished = false;
        if (speechManager.texting) {
            Debug.LogWarning("Speech manager is currently already reading a file and cannot open another one yet!");
        } else {
            closeBlocker.SetActive(true);
            speechManager.speechPrafabsIndex = prefabIndex;
            speechManager.StartTextLoop(dialogueTextFiles[index]);
            StartCoroutine(WaitForFinish());
        }
    }

    // Tells dialogue box to proceed
    public void Next() {
        if (speechManager.texting == true) {
            speechManager.next = true;
        }
    }

    // Tells dialogue box to blast through text
    public void Skip() {
        if (speechManager.texting == true) {
            speechManager.skip = true;
        }
    }

    // Listens for when speech manager spawns a new speech bubble, sets the default values and parent of the new bubble
    private void AddSpeechBubble() {
        GameObject bubble = speechManager.speechBubbles[speechManager.speechBubbles.Count - 1];
        contentRect.sizeDelta += new Vector2(0, contentRectGridBox.cellSize.y + contentRectGridBox.spacing.y);
        bubble.transform.SetParent(contentRect);
        bubble.GetComponent<RectTransform>().localScale = Vector3.one;
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        if (speechManager.speechPrafabsIndex == 2 && daisy.daisyActive) {
            daisySpriteIndex++;
            daisy.UpdateAnimator(daisyAnims[daisySpriteIndex]);
            daisy.GetComponent<WindowScript>().DetachFromHierarchy();
            daisy.GetComponent<WindowScript>().PutInFront();
            daisy.transform.position = new Vector3(daisyTeleportPoint.position.x, daisyTeleportPoint.position.y, transform.root.position.z + 10);
        }
    }

    private IEnumerator WaitForFinish() {
        while (speechManager.texting) {
            currentPrefabIndex = speechManager.speechPrafabsIndex;
            yield return null;
        }
        if (!gameEventsManager.textReadOnce) {
            gameEventsManager.TriggerEvent();
        }
        finished = true;
        gameEventsManager.textReadOnce = true;
        daisySpriteIndex = 0;
        closeBlocker.SetActive(false);
    }
}
