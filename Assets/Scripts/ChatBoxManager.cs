using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> speechPrefabs = new List<GameObject>();
    [SerializeField] public TextAsset file;
    private SpeechManager speechManager;
    private UnityEngine.RectTransform contentRect;

    private void Awake() {
        contentRect = transform.Find("Viewport/Content").GetComponent<UnityEngine.RectTransform>();
        speechManager = GetComponent<SpeechManager>();
        speechManager.speechPrefabs = speechPrefabs;
        speechManager.newBubbleCreated.AddListener(AddSpeechBubble);
    }

    private void Start() {
        speechManager.StartTextLoop(file);
    }

    private void AddSpeechBubble() {
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        GameObject bubble = speechManager.speechBubbles[speechManager.speechBubbles.Count - 1];
        contentRect.sizeDelta += new Vector2(0, bubble.GetComponent<Image>().rectTransform.sizeDelta.y + 10);
        bubble.transform.parent = contentRect;
        bubble.transform.position = new Vector3(contentRect.anchorMax.x + 400, contentRect.anchorMax.y + 250, 0);
    }
}
