using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
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
    [SerializeField] private string[] tamiTabsSTR;
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
    public TamiCourtRandomiser tamiCourtRandomiser;
    public List<GameObject> courtTamis = new List<GameObject>();

    private void Awake()
    {
        manager = GameObject.Find("ManagerOBJ").GetComponent<WindowSpawner>();
        healthDiv = rateOfHealthDepletion;
        StartCoroutine(GameTimer());
        StartCoroutine(SpawnPopUp_E());
    }
    private void Update()
    {
        UpdateBars();
        foodBarSprite.sprite = PickElement(food, foodBarSprites);
        thristBarSprite.sprite = PickElement(thirst, thirstBarSprites);
        moodBarSprite.sprite = PickElement(mood, moodBarSprites);
    }
    private IEnumerator GameTimer()
    {
        while (tamiTab == null)
        {
            yield return new WaitForEndOfFrame();
            gameTimeSinceSpawn += Time.deltaTime;
            popUpSpawnTime -= gameTimeSinceSpawn / rateOfPopUpSpawn;
            popUpSpawnTime = Mathf.Clamp(popUpSpawnTime, popUpSpawnMin, 100);
            if (gameTimeSinceSpawn > gameLength)
            {
                Destroy(gameObject);
            }
        }
    }
    public void PopUpDestroyed()
    {
        StartCoroutine(SpawnPopUp_E());
        tamiTab = null;
    }
    public IEnumerator SpawnPopUp_E()
    {
        yield return new WaitForSeconds(popUpSpawnTime);
        int num = Random.Range(0, tamiTabsGO.Length);
        SpawnPopUp(num);
    }
    public void SpawnPopUp(int num)
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
    }
    private IEnumerator GetCourtRandomiser()
    {
        yield return new WaitForSeconds(0.1f);
        tamiCourtRandomiser = FindAnyObjectByType<TamiCourtRandomiser>();
        yield return new WaitForSeconds(0.1f);
        courtTamis = tamiCourtRandomiser.allTamisSpawned;
    }
    public void PopUpYesButton1(int cost)
    {
        gold -= cost;
    }
    public void PopUpYesButton2(int index)
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
    public void PopUpYesButton3()
    {
        goldPerFrame += 0.0001f;
    }
    public void PopUpYesButton4(int add)
    {
        gold += add;
    }
    public void PopUpNoButton1()
    {
        food -= 40;
        thirst -= 40;
        mood -= 40;
    }
    public void CourtCheckGuilty(bool guilty)
    {
        foreach (GameObject go in courtTamis)
        {
            Destroy(go);
        }
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
    private void UpdateBars()
    {
        if (food <= 0 && thirst <= 0 && mood <= 0)
        {
            Debug.Log("All bars empty");
            // Health goes down faster when all bars are empty
            health -= Time.deltaTime / healthDiv * 200; // +100% Speed!
            healthBarSprite.fillAmount = health / 100;

            if (health <= 0)
            {
                TamiReviveEvent();
            }
        }
        else if (food <= 0 || thirst <= 0 || mood <= 0)
        {
            Debug.Log("One bar empty");
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
        // No idea why unity doesnt have their Mathf.Round() use decimal place rounding too
        goldText.text = "Gold: " + System.Math.Round(gold, 2).ToString();
    }
    // All buttons/tami event thingys
    public void FeedTami(bool fromPopUp)
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
    private void TamiReviveEvent()
    {
        Destroy(tamiTab);
        healthDiv += 30;
        health = 100;
        reviveCounter++;
    }
    private void OnDestroy()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
