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
    [HideInInspector] public int startPrefabIndex = 0;
    [HideInInspector] public bool readOnStart = false;

    public List<TextAsset> dialogueTextFiles = new List<TextAsset>();

    private SpeechManager speechManager; // Stores the speech manager script attatched to the chatbox
    private RectTransform contentRect; // Stores the content UI, this is where the speech bubbles get placed on to be scrolled through
    private GridLayoutGroup contentRectGridBox;

    private void Awake() {
        contentRect = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        contentRectGridBox = contentRect.GetComponent<GridLayoutGroup>();
        speechManager = GetComponent<SpeechManager>();
        speechManager.newBubbleCreated.AddListener(AddSpeechBubble);
    }

    private void Start() {
        if (readOnStart) {
            StartText(fileIndex, startPrefabIndex);
        }
    }

    // If not already reading, read file at given index
    public void StartText(int index, int prefabIndex) {
        if (speechManager.texting) {
            Debug.LogWarning("Speech manager is currently already reading a file and cannot open another one yet!");
        } else {
            speechManager.speechPrafabsIndex = prefabIndex;
            speechManager.StartTextLoop(dialogueTextFiles[index]);
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
        bubble.transform.parent = contentRect;
        bubble.GetComponent<RectTransform>().localScale = Vector3.one;
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
