using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
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
    public void CheckWhatWeHit()
    {
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, cam);
        if (wordIndex != -1)
        {
            TMP_WordInfo wordInfo = text.textInfo.wordInfo[wordIndex];
            wordWeHit = wordInfo.GetWord();

            // Check the color of the first character in the word
            int firstCharIndex = wordInfo.firstCharacterIndex;
            TMP_CharacterInfo charInfo = text.textInfo.characterInfo[firstCharIndex];

            // get the color from one of the character's vertices (they should all match)
            Color32 color = text.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex];
            if (checkColor)
            {
                if (!color.Equals(new Color32(255, 255, 255, 255)))
                {
                    downloader.DownloadFile();
                }
                
            }
            else
            {
                //spawn browser and change website to link
            }
        }
    }
}
