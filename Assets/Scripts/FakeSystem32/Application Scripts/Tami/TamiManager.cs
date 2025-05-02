using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge, George Chapple
    Purpose           : For the tami minigame
*/
public class TamiManager : MonoBehaviour
{
    [Header("Important Settings")]
    [SerializeField] private string sceneName;
    [Tooltip("This is the time in seconds for how fast the timer runs out. Base = 120 | 2 Minutes.")]
    [SerializeField] private float rateOfHealthDepletion = 120;
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Image foodBarSprite;
    [SerializeField] private Image thristBarSprite;
    [SerializeField] private Image moodBarSprite;
    [SerializeField] private Image moodBarImg;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Sprite[] foodBarSprites;
    [SerializeField] private Sprite[] thirstBarSprites;
    [SerializeField] private Sprite[] moodBarSprites;
    [SerializeField] private GameObject[] tamiTabsGO;
    [SerializeField] private GameObject tamiTab;


    [Header("Time in seconds before full bar is empty.")]
    [SerializeField] private float rateOfFoodDepletion;
    [SerializeField] private float rateOfThirstDepletion;
    [SerializeField] private float rateOfMoodDepletion;

    [SerializeField] private float foodAddAmount;
    [SerializeField] private float thirstAddAmount;
    [SerializeField] private float moodAddAmount;
    [SerializeField] private float goldPerFrame = 0.001f;

    [SerializeField] private float popUpSpawnTime = 15;
    [SerializeField] private float popUpSpawnMin = 5;
    [Tooltip("This dictates how fast the pop ups will spawn based on an in game timer. The formula is dumb but this var is subtracted from gameTimeSinceSpawn (The lower it is the faster it is).")]
    [SerializeField] private float rateOfPopUpSpawn = 50000;
    [Tooltip("DO NOT CHANGE!")]
    [SerializeField] private float gameTimeSinceSpawn = 0;
    [Tooltip("The game will end once this amount of seconds pass;")]
    [SerializeField] private float gameLength = 300;


    [Header("Serialisations")]
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private WindowSpawner manager;

    [HideInInspector] public float gold = 0;
    private float healthDiv = 120;
    private float health = 100;
    private float food = 100;
    private float thirst = 100;
    private float mood = 100;

    private float reviveCounter = 0;
    private GameEventsManager gameEventsManager;
    public TamiCourtRandomiser tamiCourtRandomiser;
    public List<GameObject> courtTamis = new List<GameObject>();

    private void Awake() // Set Up
    {
        gameEventsManager = GameObject.Find("ManagerOBJ").GetComponent<GameEventsManager>();
        manager = GameObject.Find("ManagerOBJ").GetComponent<WindowSpawner>();
        healthDiv = rateOfHealthDepletion;
        StartCoroutine(GameTimer());
        StartCoroutine(SpawnPopUp_E());
    }
    private void Update() // Update all bars and values
    {
        UpdateBars();
        foodBarSprite.sprite = PickElement(food, foodBarSprites);
        thristBarSprite.sprite = PickElement(thirst, thirstBarSprites);
        moodBarSprite.sprite = PickElement(mood, moodBarSprites);
    }
    private IEnumerator GameTimer() // Game time in real time, affects how frequently pop ups spawn
    {                               // it does stop once a tab exists tho so it'll be out of sync
        while (tamiTab == null)
        {
            yield return new WaitForEndOfFrame();
            gameTimeSinceSpawn += Time.deltaTime;
            popUpSpawnTime -= gameTimeSinceSpawn / rateOfPopUpSpawn;
            popUpSpawnTime = Mathf.Clamp(popUpSpawnTime, popUpSpawnMin, 100);
            if (gameTimeSinceSpawn > gameLength)
            {
                if (gameEventsManager != null) {
                    gameEventsManager.NextDialogue(6, true, false);
                }
                Destroy(gameObject);
            }
        }
    }
    public void PopUpDestroyed() // Starts timers back up
    {
        StartCoroutine(SpawnPopUp_E());
        tamiTab = null;
        StartCoroutine(GameTimer());
    }
    public IEnumerator SpawnPopUp_E() // Spawns popup after pre computed time
    {
        yield return new WaitForSeconds(popUpSpawnTime);
        int num = Random.Range(0, tamiTabsGO.Length);
        SpawnPopUp(num);
    }
    public void SpawnPopUp(int num) // Spawns a popup
    {
        if (tamiTab != null)
        {
            // Destroy if already there
            Destroy(tamiTab);
            tamiTab = null;
        }
        // Spawn Pop Up
        GameObject newPopUp = Instantiate(tamiTabsGO[num], contentPanel.transform);
        newPopUp.transform.SetAsLastSibling();

        if (num == 4)
        {
            StartCoroutine(GetCourtRandomiser());
        }
        tamiTab = newPopUp;
    }
    private IEnumerator GetCourtRandomiser() // This is super janky, but its in another scene and it needed time to load.
    {                                        // I technically could've done it in the scene but its too late now.
        yield return new WaitForSeconds(0.1f);
        tamiCourtRandomiser = FindAnyObjectByType<TamiCourtRandomiser>();
        yield return new WaitForSeconds(0.1f);
        courtTamis = tamiCourtRandomiser.allTamisSpawned;
        tamiTab.GetComponent<TamiPopUpScript>().CourtSetUp(tamiCourtRandomiser.guilty);
    }
    // All buttons effects
    public void PopUpYesButton1(int cost) // Removes Gold
    {
        gold -= cost;
    }
    public void PopUpYesButton2(int index) // Maximises one of tami's stats
    {
        switch (index)
        {
            case 0:
                FeedTami(true);
                break;
            case 1:
                HydrateTami(true);
                break;
            case 2:
                PlayWithTami(true);
                break;
            default:
                break;
        }
    }
    public void PopUpYesButton3() // For model scene
    {
        goldPerFrame += 0.0001f;
    }
    public void PopUpYesButton4(int add) // Adds Gold
    {
        gold += add;
    }
    public void PopUpNoButton1() // Halves all stats
    {
        food -= food / 2;
        thirst -= thirst / 2;
        mood -= mood / 2;
    }
    public void CourtCheckGuilty(bool guilty) // Check if guilty
    {
        foreach (GameObject go in courtTamis) // Destroy all tamis spawned by court
        {
            Destroy(go);
        }
        courtTamis.Clear();
        if (tamiCourtRandomiser.guilty)
        {
            if (!guilty) { PopUpNoButton1(); }
            else { PopUpYesButton4(2); }
        }
        else
        {
            if (!guilty) { PopUpYesButton4(2); }
            else { PopUpNoButton1(); }
        }
    }
    private void UpdateBars() // Updates all stats and UI, also logic on stats
    {
        if (food <= 0 && thirst <= 0 && mood <= 0) // All stat empty, 2x speed
        {
            // Health goes down faster when all bars are empty
            health -= Time.deltaTime / healthDiv * 200; // +100% Speed!
            healthBarSprite.fillAmount = health / 100;

            if (health <= 0)
            {
                TamiReviveEvent();
            }
        }
        else if (food <= 0 || thirst <= 0 || mood <= 0) // One stat empty, diminish health
        {
            // Health bar should take 2 minutes to deplete
            // Since its 1 it needs to be divided by the amount of seconds
            // Because 1 Time.deltaTime unit is one second
            health -= Time.deltaTime / healthDiv * 100;
            healthBarSprite.fillAmount = health / 100;

            food -= Time.deltaTime / (rateOfFoodDepletion * 1.5f / 100);
            thirst -= Time.deltaTime / (rateOfThirstDepletion * 1.5f / 100);
            mood -= Time.deltaTime / (rateOfMoodDepletion * 1.5f / 100);
            moodBarImg.fillAmount = mood / 100;
        }
        else
        {
            // Since the base value for food is 100 not 1,
            // Rate needs to be divided by it to make it 1
            // Then it will work the same way as the health bar timer
            food -= Time.deltaTime / (rateOfFoodDepletion / 100);
            thirst -= Time.deltaTime / (rateOfThirstDepletion / 100);
            mood -= Time.deltaTime / (rateOfMoodDepletion / 100);
            moodBarImg.fillAmount = mood / 100;
        }
        gold += goldPerFrame;
        OnGoldValChanged();
    }
    // Chooses a sprite to use based on a value using a percentage out of 100
    public Sprite PickElement(float value, Sprite[] sprites) 
    {
        value = Mathf.Clamp(value, 0f, 100f); // Ensure 0-100
        int index = Mathf.FloorToInt((value / 100f) * sprites.Length);

        // Force index to stay in bounds
        index = Mathf.Min(index, sprites.Length - 1);

        return sprites[index];
    }
    private void OnGoldValChanged()
    {
        // No idea why unity doesnt have a decimal place round too
        goldText.text = "Gold: " + System.Math.Round(gold, 2).ToString();
    }
    // All tami event thingys
    public void FeedTami(bool fromPopUp) // Add to food by set amount and clamp val
    {
        food = Mathf.Clamp(food + foodAddAmount, 0, 100);
        if (fromPopUp) { food = 100; }
    }
    public void HydrateTami(bool fromPopUp)
    {
        thirst = Mathf.Clamp(thirst + thirstAddAmount, 0, 100);
        if (fromPopUp) { thirst = 100; }
    }
    public void PlayWithTami(bool fromPopUp)
    {
        mood = Mathf.Clamp(mood + moodAddAmount, 0, 100);
        if (fromPopUp) { mood = 100; }
    }
    private void TamiReviveEvent() // Revives Tami with full health and half stats
    {
        Destroy(tamiTab);
        tamiTab = null;
        healthDiv += 30;
        health = 100;
        food = 50;
        thirst = 50;
        mood = 50;
        reviveCounter++;
    }
    private void OnDestroy() // Remove Tami scene when tab destroyed
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
