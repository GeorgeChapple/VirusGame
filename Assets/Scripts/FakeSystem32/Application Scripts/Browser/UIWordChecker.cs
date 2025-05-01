using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To "download" a word from a website if its revealed by the flashlight
*/
public class UIWordChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Camera cam;
    [SerializeField] private Downloader downloader;
    private string wordWeHit;
    public string link;
    public bool checkColor = true;

    private void Awake()
    {
        cam = Camera.main;
    }
    // Checks what word was hit using mouse position, called by events
    public void CheckWhatWeHit()
    {
        // Get the index of the word the mouse is over
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, cam);
        if (wordIndex != -1)
        {
            // Get the word out from the index
            TMP_WordInfo wordInfo = text.textInfo.wordInfo[wordIndex];
            wordWeHit = wordInfo.GetWord();

            // Check the color of the first character in the word
            int firstCharIndex = wordInfo.firstCharacterIndex;
            TMP_CharacterInfo charInfo = text.textInfo.characterInfo[firstCharIndex];

            // Get the color from one of the character's vertices (they should all match)
            Color32 color = text.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex];
            if (checkColor)
            {
                if (!color.Equals(new Color32(255, 255, 255, 255)))
                {
                    downloader.DownloadFile();
                }
                
            }
        }
    }
}
