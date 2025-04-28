using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TamiManager : MonoBehaviour
{
    [Header("Important Settings")]
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
    [SerializeField] private Material[] tamiTabsMAT;


    [Header("Time in seconds before full bar is empty.")]
    [SerializeField] private float rateOfFoodDepletion;
    [SerializeField] private float rateOfThirstDepletion;
    [SerializeField] private float rateOfMoodDepletion;

    [SerializeField] private float foodAddAmount;
    [SerializeField] private float thirstAddAmount;
    [SerializeField] private float moodAddAmount;

    [SerializeField] private float popUpSpawnTime = 15;
    [SerializeField] private float popUpSpawnMin = 5;
    [Tooltip("This dictates how fast the pop ups will spawn based on an in game timer. The formula is dumb but this var is subtracted from gameTimeSinceSpawn (The lower it is the faster it is).")]
    [SerializeField] private float rateOfPopUpSpawn = 50000;
    [Tooltip("DO NOT CHANGE!")]
    [SerializeField] private float gameTimeSinceSpawn = 0;


    [Header("Serialisations")]
    [SerializeField] private AdditiveSceneHandler additiveSceneHandler;
    [SerializeField] private WindowSpawner manager;

    private float gold = 0;
    private float healthDiv = 120;
    private float health = 100;
    private float food = 100;
    private float thirst = 100;
    private float mood = 100;

    private float reviveCounter = 0;

    private void Awake()
    {
        manager = GameObject.Find("ManagerOBJ").GetComponent<WindowSpawner>();
        healthDiv = rateOfHealthDepletion;
        StartCoroutine(GameTimer());
        StartCoroutine(SpawnPopUpRepeated());
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
        while (true)
        {
            yield return new WaitForEndOfFrame();
            gameTimeSinceSpawn += Time.deltaTime;
            popUpSpawnTime -= gameTimeSinceSpawn / rateOfPopUpSpawn;
            popUpSpawnTime = Mathf.Clamp(popUpSpawnTime, popUpSpawnMin, 100);
        }
    }
    private IEnumerator SpawnPopUpRepeated()
    {
        while (true)
        {
            yield return new WaitForSeconds(popUpSpawnTime);
            Debug.Log("spawn");
            if (tamiTabsGO.Length > 0)
            {
                int num = Random.Range(0, tamiTabsGO.Length);
                SpawnPopUp(num);
            }
        }
    }
    public void SpawnPopUp(int num)
    {
        // Do additive stuff first
        SceneManager.LoadScene(tamiTabsSTR[num], LoadSceneMode.Additive);

        manager.sceneName = tamiTabsSTR[num];
        manager.cameraMaterial = tamiTabsMAT[num];
        manager.SpawnWindow(tamiTabsGO[num]);
        //additiveSceneHandler.cameraMaterial = tamiTabsMAT[num];
        //GameObject newPopUp = Instantiate(tamiTabsGO[num]);
        //newPopUp.GetComponent<WindowContent>().SetManager(manager);
        //newPopUp.GetComponent<WindowContent>().OnceSpawned();
        // Do stuff once spawned
    }
    private void UpdateBars()
    {
        if (food <= 0 && thirst <= 0 && mood <= 0)
        {
            Debug.Log("All bars empty");
            // Health goes down faster when all bars are empty
            health -= Time.deltaTime / healthDiv * 200; // +100% Speed!
            healthBarSprite.fillAmount = health;

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
            healthBarSprite.fillAmount = health;

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
        goldText.text = "Gold: " + gold.ToString();
    }
    // All buttons/tami event thingys
    public void FeedTami()
    {
        food = Mathf.Clamp(food + foodAddAmount, 0, 100);
    }
    public void HydrateTami()
    {
        thirst = Mathf.Clamp(thirst + thirstAddAmount, 0, 100);
    }
    public void PlayWithTami()
    {
        mood = Mathf.Clamp(mood + moodAddAmount, 0, 100);
    }
    private void TamiReviveEvent()
    {
        healthDiv += 30;
        health = 100;
        reviveCounter++;
    }
}
