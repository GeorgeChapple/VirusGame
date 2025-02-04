using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxManager : MonoBehaviour
{
    private SpeechManager speechManager; // Stores the speech manager script attatched to the chatbox
    private UnityEngine.RectTransform contentRect; // Stores the content UI, this is where the speech bubbles get placed on to be scrolled through

    private void Awake() {
        contentRect = transform.Find("Viewport/Content").GetComponent<UnityEngine.RectTransform>();
        speechManager = GetComponent<SpeechManager>();
        speechManager.newBubbleCreated.AddListener(AddSpeechBubble);
    }

    private void Start() {
        speechManager.StartTextLoop(speechManager.file);
    }

    // Listens for when speech manager spawns a new speech bubble, sets the default values and parent of the new bubble
    private void AddSpeechBubble() {
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        GameObject bubble = speechManager.speechBubbles[speechManager.speechBubbles.Count - 1];
        contentRect.sizeDelta += new Vector2(0, bubble.GetComponent<Image>().rectTransform.sizeDelta.y + 5);
        bubble.transform.parent = contentRect;
        //bubble.transform.position = new Vector3(contentRect.anchorMax.x + 400, contentRect.anchorMax.y + 250, 0);
    }
}
