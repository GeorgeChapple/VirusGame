using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class GameEventsManager : MonoBehaviour {
    [HideInInspector] public string default_SaveFilePath = "default_";
    [HideInInspector] public string icons_SaveFilePath = "icons.txt";
    [HideInInspector] public string dialogue_SaveFilePath = "dialogue.txt";
    [HideInInspector] public string reset_SaveFilePath = "ResetGame.txt";
    [HideInInspector] public List<GameObject> desktopIcons = new List<GameObject>();
    [HideInInspector] public int totalDialogue = 12;
    [HideInInspector] public int dialoguePrefabIndex;
    [HideInInspector] public SoundScript soundScript;
    [HideInInspector] public bool textReadOnce = false;

    [SerializeField] private List<Website> articles = new List<Website>();
    public string daisyFileName = "DAISY_Master";

    public bool useDefaults;
    public int dialogueIndex;

    public DaisyScript daisy;

    private bool checkingPlatformer = false;
    private bool checkingDelete = false;

    [Tooltip("This var needs to have the desktop fileData file in it")]
    public FileData deskTopFileDirectory;

    private void Awake() {
        PlayerPrefs.SetInt("PlatformPuzzleWin", 0);
        daisy = FindFirstObjectByType<DaisyScript>();
        soundScript = GetComponent<SoundScript>();
        using (StreamReader sr = new StreamReader(reset_SaveFilePath)) {
            if (sr.ReadLine()[0] == '1') {
                useDefaults = true;
            }
        }
        if (useDefaults) {
            DefaultOverwriteFiles(icons_SaveFilePath);
            DefaultOverwriteFiles(dialogue_SaveFilePath);
            using (StreamWriter sw = new StreamWriter(reset_SaveFilePath)) {
                sw.WriteLine("0 - Change this to 1 if you want to reset your save data.");
            }
        }
        using (StreamReader sr = new StreamReader(dialogue_SaveFilePath)) {
            string line; 
            line = sr.ReadLine();
            dialogueIndex = int.Parse(line); 
            for (int i = 0; i <= dialogueIndex; i++) {
                line = sr.ReadLine();
            }
            Debug.Log("Dialogue Prefab Index " + line[0].ToString());
            dialoguePrefabIndex = int.Parse(line[0].ToString());
        }
    }

    private void Start() {
        StartCoroutine(WaitForEmail(5, false));
        StartCoroutine(WaitForStartPopUp(5));
    }

    public void TriggerEvent() {
        Debug.Log("Dialogue Index : " + dialogueIndex);
        if (dialogueIndex >= 11) {
            Application.Quit();
        }
        if (dialogueIndex >= 10) {
            NextDialogue(10, false, false);
            StartCoroutine(WaitForEmail(10, true));
        }
        if (dialogueIndex >= 9) {
            if (dialogueIndex == 9) {
                StartCoroutine(ChangeTheScene("EndPuzzle1"));
            }
        }
        if (dialogueIndex >= 8) {
            if (dialogueIndex == 8) {
                StartCoroutine(CheckForDaisyDelete());
            }
        }
        if (dialogueIndex >= 7) {
            if (dialogueIndex == 7) {
                Events_Generating();
            }
        }
        if (dialogueIndex >= 6) {
            if (dialogueIndex == 6) {
                Events_EnableTami();
            }
        }
        if (dialogueIndex >= 5) {
            if (dialogueIndex == 5) {
                NextDialogue(5, false, false);
                StartCoroutine(WaitForEmail(5, true));
            }
        }
        if (dialogueIndex >= 3) {
            if (!checkingPlatformer && dialogueIndex < 5) {
                StartCoroutine(CheckPlatformerComplete());
            }
        }
        if (dialogueIndex >= 2) {
            if (dialogueIndex == 2) {
                NextDialogue(2, false, false);
                StartCoroutine(WaitForEmail(10, true));
            }
        }
        if (dialogueIndex >= 1) {
            if (daisy != null) {
                if (!daisy.daisyActive && dialogueIndex < 7) {
                    Events_Download();
                }
            }
        }
        if (dialogueIndex >= 0) {
            Events_OpenArticle();
        }
    }

    public void NextDialogue(int eventIndex, bool notify, bool set) {
        if (dialogueIndex == eventIndex || set) {
            textReadOnce = false;
            if (set) {
                dialogueIndex = eventIndex;
            } else {
                dialogueIndex++;
            }
            string[] text = File.ReadAllLines(dialogue_SaveFilePath);
            text[0] = dialogueIndex.ToString();
            File.WriteAllLines(dialogue_SaveFilePath, text);
            dialoguePrefabIndex = Mathf.Clamp(int.Parse(text[dialogueIndex + 1][0].ToString()), 0, totalDialogue - 1);
            if (notify) {
                EmailNotif();
            }
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

    private void EmailNotif() {
        foreach (GameObject obj in desktopIcons) {
            if (obj.name == "Email") {
                obj.GetComponent<SpriteHandlerScript>().spriteIndex = 2;
                obj.GetComponent<SpriteHandlerScript>().RefreshSprite();
                soundScript.PlaySound(0, 1, 1);
            }
        }
    }

    private void EnableDesktopIcon(string icon) {
        string[] iconText = File.ReadAllLines(icons_SaveFilePath);
        int i = 0;
        foreach (string line in iconText) {
            if (line.Contains(icon)) {
                iconText[i] = "1 - " + line.Substring(4, line.Length - 4);
            }
            i++;
        }
        File.WriteAllLines(icons_SaveFilePath, iconText);
    }

    private void Events_OpenArticle() {
        foreach (Website wb in articles) {
            wb.active = true;
        }
    }

    private void Events_Download() {
        EnableDesktopIcon("Platformer");
        NextDialogue(1, false, false);
        StartCoroutine(ChangeTheScene("DownloadDAISY"));
    }

    private void Events_EnableTami() {
        EnableDesktopIcon("Tami"); 
        StartCoroutine(ChangeTheScene("UI_Test 1"));
    }

    private void Events_Generating() {
        NextDialogue(7,false, false);
        StartCoroutine(ChangeTheScene("GeneratingNewPuzzles"));
    }

    private IEnumerator WaitForEmail(float time, bool forceNotify) {
        yield return new WaitForSeconds(time);
        if (FindObjectsByType<ChatBoxManager>(FindObjectsSortMode.None).Length <= 0 || forceNotify) {
            EmailNotif();
        }
    }

    private IEnumerator WaitForStartPopUp(float time) {
        yield return new WaitForSeconds(time);
        PopUpManager popUpManager = GetComponent<PopUpManager>();
        if (popUpManager != null && daisy.daisyActive) {
            popUpManager.StartSpawningPopUps();
        }
    }

    private IEnumerator ChangeTheScene(string sceneName) {
        Debug.Log(sceneName + " loaded...");
        while (daisy.chatBoxActive) {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator CheckPlatformerComplete() {
        bool check = true;
        checkingPlatformer = true;
        while (check) {
            int value = PlayerPrefs.GetInt("PlatformPuzzleWin");
            if (value == 1) {
                NextDialogue(4, true, true);
                PlayerPrefs.SetInt("PlatformPuzzleWin", 0);
                check = false;
            } else if (value == 2) {
                NextDialogue(5, true, true);
                PlayerPrefs.SetInt("PlatformPuzzleWin", 0);
                check = false;
            }
            yield return null;
        }
        checkingPlatformer = false;
    }

    private IEnumerator CheckForDaisyDelete() {
        if (!checkingDelete) {
            checkingDelete = true;
            while (Commands.CheckForFileOnDesktop(daisyFileName)) {
                yield return null;
            }
            checkingDelete = false;
            NextDialogue(9, false, true);
            StartCoroutine(ChangeTheScene("UI_Test 1"));
        }
        yield return null;
    }
}
