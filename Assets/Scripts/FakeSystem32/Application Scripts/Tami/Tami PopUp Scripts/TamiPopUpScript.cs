using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : Spawns tami scenes and sets up buttons to interact with said scenes
*/
public class TamiPopUpScript : MonoBehaviour
{
    [SerializeField] private TamiManager tamiManager;
    [SerializeField] private string sceneName;
    [SerializeField] private UnityEvent yesEvent;
    [SerializeField] private UnityEvent noEvent;
    [SerializeField] private List<EventPass> eventpassYes;
    [SerializeField] private List<EventPass> eventpassNo;
    [SerializeField] private float cost;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private string textToSpawnGood;
    [SerializeField] private string textToSpawnCantAfford;
    [SerializeField] private string textToSpawnBad;
    private TamiCourtRandomiser tamiCourtRandomiser;
    [HideInInspector] public bool guilty;
    private void Awake() // Set up scene, set up buttons(uses eventpass rather than making a new seperate one)
    {        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        tamiManager = GetComponentInParent<TamiManager>();
        tamiCourtRandomiser = FindAnyObjectByType<TamiCourtRandomiser>();
        foreach (EventPass pass in eventpassYes)
        {
            if (pass.passIntVal)
            {
                yesEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, pass.intVal); });
                continue;
            }
            if (pass.passBoolVal)
            {
                yesEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, pass.boolVal); });
                continue;
            }
            yesEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, cost); });
        }
        foreach (EventPass pass in eventpassNo)
        {
            if (pass.passIntVal)
            {
                noEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, pass.intVal); });
                continue;
            }
            if (pass.passBoolVal)
            {
                noEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, pass.boolVal); });
                continue;
            }
            noEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, cost); });
        }
    }
    private void Start() => tamiCourtRandomiser = FindAnyObjectByType<TamiCourtRandomiser>();
    public void GoodButton() // Called by the left button on pop up
    {
        if (tamiManager.gold >= cost)
        {
            yesEvent.Invoke();
            GameObject textObj = Instantiate(textPrefab, transform.parent);
            textObj.GetComponent<FloatingText>().ChangeText(textToSpawnGood);
            PopUpDestroy();
        }
        else
        {
            GameObject textObj = Instantiate(textPrefab, transform.parent);
            textObj.GetComponent<FloatingText>().ChangeText(textToSpawnCantAfford);
        }
    }
    public void BadButton() // Called by the right button on pop up
    {
        noEvent.Invoke();
        GameObject textObj = Instantiate(textPrefab, transform.parent);
        textObj.GetComponent<FloatingText>().ChangeText(textToSpawnBad);
        PopUpDestroy();
    }
    public void CourtSetUp(bool _guilty) // Changes text on spawn
    {
        guilty = _guilty;
        if (guilty) 
        {
            textToSpawnGood = "She was Guilty, All stats halved";
            textToSpawnBad = "She was Guilty, 2 Gold added";
        }
        else
        {
            textToSpawnGood = "She was Innocent, 2 Gold added";
            textToSpawnBad = "She was Innocent, All stats halved";
        }
    }
    public void PopUpDestroy() // Destroy Tab after unloading scene and turning timers back on
    {
        tamiManager.PopUpDestroyed();
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        Destroy(gameObject);
    }
}
