using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float yAxisChangePerFrame = 0.1f;
    [SerializeField] private float lifeTime;
    [SerializeField] private byte r = 255; // Color wasnt working with floats (Wouldn't change alpha)
    [SerializeField] private byte g = 255; // However, changing to bytes for Color32 did
    [SerializeField] private byte b = 255;
    public byte currentAlpha = 255;

    private void Awake()
    {
        currentAlpha = 255;
        Destroy(gameObject, lifeTime);
        StartCoroutine(MoveAndDim());
    }
    IEnumerator MoveAndDim()
    {
        while (true)
        {
            rectTransform.anchoredPosition += new Vector2(0, yAxisChangePerFrame);
            currentAlpha = (byte)Mathf.Lerp(currentAlpha, 0f, Time.deltaTime * 4);
            text.color = new Color32(r, g, b, currentAlpha);
            image.color = new Color32(0,0,0, currentAlpha);
            yield return null;
        }
    }
    public void ChangeText(string newText)
    {
        text.text = newText;
    }
}
