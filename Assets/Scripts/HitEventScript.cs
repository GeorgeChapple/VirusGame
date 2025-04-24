using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
*/

public class HitEventScript : MonoBehaviour
{
    public UnityEvent hitEvent;
    public UnityEvent doubleHitEvent;
    public UnityEvent letGoEvent;

    public bool hovering = false;
    public GameObject hoveringObject;

    private float timer = 0.5f;
    public float timeToDoubleHit = 0.5f;

    public bool doubleAvailable;
    public void CheckIfDouble()
    {
        if (!IfActive())
        {
            return;
        }
        if (doubleAvailable)
        {
            //fire the double hit
            doubleHitEvent.Invoke();
            timer = 0;
        }
    }
    public void StartDoubleTimer()
    {
        if (!IfActive())
        {
            return;
        }
        if (doubleAvailable) { return; }
        timer = timeToDoubleHit;
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        doubleAvailable = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        doubleAvailable = false;
        yield return null;
    }
    private bool IfActive()
    {
        if (!gameObject.activeInHierarchy) { return false; }
        return true;
    }
}
