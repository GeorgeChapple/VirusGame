using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class GameEventsManager : MonoBehaviour {
    [HideInInspector] public string default_SaveFilePath = "default_";
    [HideInInspector] public string icons_SaveFilePath = "icons.txt";
    [HideInInspector] public string dialogue_SaveFilePath = "dialogue.txt";
    [HideInInspector] public string events_SaveFilePath = "events.txt";
    [HideInInspector] public List<GameObject> desktopIcons = new List<GameObject>();
    [HideInInspector] public int totalDialogue = 12;
    [HideInInspector] public int dialoguePrefabIndex;
    [HideInInspector] public SoundScript soundScript;

    [SerializeField] private List<Website> articles = new List<Website>();

    public bool useDefaults;
    public int dialogueIndex;

    private DaisyScript daisy;

    [Tooltip("This var needs to have the desktop fileData file in it")]
    public FileData deskTopFileDirectory;

    private void Awake() {
        daisy = FindFirstObjectByType<DaisyScript>();
        soundScript = GetComponent<SoundScript>();
        if (useDefaults) {
            DefaultOverwriteFiles(icons_SaveFilePath);
            DefaultOverwriteFiles(dialogue_SaveFilePath);
            DefaultOverwriteFiles(events_SaveFilePath);
        }
        using (StreamReader sr = new StreamReader(dialogue_SaveFilePath)) {
            string line; 
            line = sr.ReadLine();
            dialogueIndex = int.Parse(line); 
            line = sr.ReadLine();
            dialoguePrefabIndex = int.Parse(line[0].ToString());
        }
    }

    private void Start() {
        StartCoroutine(wait());
    }

    public void TriggerEvent() {
        if (dialogueIndex > 0) {
            Events_OpenArticle();
        }
    }

    public void NextDialogue(int eventIndex) {
        if (dialogueIndex == eventIndex) {
            dialogueIndex++;
            string[] text = File.ReadAllLines(dialogue_SaveFilePath);
            text[0] = dialogueIndex.ToString();
            File.WriteAllLines(dialogue_SaveFilePath, text);
            dialoguePrefabIndex = Mathf.Clamp(int.Parse(text[dialogueIndex + 1][0].ToString()), 0, totalDialogue - 1);
        }
    }

    private void DefaultOverwriteFiles(string filePath) {
        string line;
        using (StreamReader sr = new StreamReader(default_SaveFilePath + filePath)) {
            using (StreamWriter sw = new StreamWriter(filePath)) {
                while ((line = sr.ReadLine()) != null) {
                    sw.WriteLine(line);
                }
            }
        }
    }

    private IEnumerator wait() {
        yield return new WaitForSeconds(5);
        EmailNotif();
    }

    private void EmailNotif() {
        foreach (GameObject obj in desktopIcons) {
            if (obj.name == "Email") {
                obj.GetComponent<SpriteHandlerScript>().spriteIndex = 2;
                obj.GetComponent<SpriteHandlerScript>().RefreshSprite();
                soundScript.PlaySound(0, 1, 1);
            }
        }
    }

    private void Events_OpenArticle() {
        foreach (Website wb in articles) {
            wb.active = true;
        }
    }
}
