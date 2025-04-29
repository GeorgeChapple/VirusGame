using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
public class TamiPopUpScript : MonoBehaviour
{
    [SerializeField] private TamiManager tamiManager;
    [SerializeField] private string sceneName;
    [SerializeField] private UnityEvent yesEvent;
    [SerializeField] private List<EventPass> eventpass;
    [SerializeField] private float cost;
    private void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        tamiManager = GetComponentInParent<TamiManager>();
        foreach (EventPass pass in eventpass)
        {
            if (pass.passIntVal)
            {
                yesEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, pass.intVal); });
                continue;
            }
            yesEvent.AddListener(delegate { tamiManager.SendMessage(pass.methodName, cost); });
        }
    }
    public void GoodButton()
    {
        if (tamiManager.gold >= cost)
        {
            yesEvent.Invoke();
            Destroy(gameObject);
        }
    }
    public void BadButton()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        tamiManager.PopUpDestroyed();
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
