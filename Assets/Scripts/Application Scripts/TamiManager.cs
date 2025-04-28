using System;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
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

    [Header("Time in seconds before full bar is empty.")]
    [SerializeField] private float rateOfFoodDepletion;
    [SerializeField] private float rateOfThirstDepletion;
    [SerializeField] private float rateOfMoodDepletion;

    [SerializeField] private float foodAddAmount;
    [SerializeField] private float thirstAddAmount;
    [SerializeField] private float moodAddAmount;
    
    

    private float gold = 0;
    private float healthDiv = 120;
    private float health = 100;
    private float food = 100;
    private float thirst = 100;
    private float mood = 100;

    private void Awake()
    {
        healthDiv = rateOfHealthDepletion;
    }
    private void Update()
    {

        UpdateBars();
        foodBarSprite.sprite = PickElement(food, foodBarSprites);
        thristBarSprite.sprite = PickElement(thirst, thirstBarSprites);
        moodBarSprite.sprite = PickElement(mood, moodBarSprites);
    }
    private void UpdateBars()
    {
        // Health bar should take 2 minutes to deplete
        // Since its 1 it needs to be divided by the amount of seconds
        // Because 1 Time.deltaTime unit is one second
        health -= Time.deltaTime / healthDiv;
        healthBarSprite.fillAmount = health;

        // Since the base value for food is 100 not 1,
        // Rate needs to be divided by it to make it 1
        // Then it will work the same way as the health bar timer
        food -= Time.deltaTime / (rateOfFoodDepletion / 100);
        thirst -= Time.deltaTime / (rateOfThirstDepletion / 100);
        mood -= Time.deltaTime / (rateOfMoodDepletion / 100);
        moodBarImg.fillAmount = mood / 100;
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
        goldText.text = gold.ToString();
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
}
