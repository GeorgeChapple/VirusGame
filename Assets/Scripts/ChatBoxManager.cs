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
    private SpeechManager speechManager; // Stores the speech manager script attatched to the chatbox
    private RectTransform contentRect; // Stores the content UI, this is where the speech bubbles get placed on to be scrolled through
    private GridLayoutGroup contentRectGridBox;

    private void Awake() {
        contentRect = transform.Find("Viewport/Content").GetComponent<UnityEngine.RectTransform>();
        contentRectGridBox = contentRect.GetComponent<GridLayoutGroup>();
        speechManager = GetComponent<SpeechManager>();
        speechManager.newBubbleCreated.AddListener(AddSpeechBubble);
    }

    private void Start() {
        speechManager.StartTextLoop(speechManager.file);
    }

    // Listens for when speech manager spawns a new speech bubble, sets the default values and parent of the new bubble
    private void AddSpeechBubble() {
        GameObject bubble = speechManager.speechBubbles[speechManager.speechBubbles.Count - 1];
        contentRect.sizeDelta += new Vector2(0, contentRectGridBox.cellSize.y + contentRectGridBox.spacing.y);
        bubble.transform.parent = contentRect;
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
