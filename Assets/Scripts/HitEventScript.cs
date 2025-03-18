using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class HitEventScript : MonoBehaviour
{
    public UnityEvent hitEvent;
    public UnityEvent doubleHitEvent;
    public UnityEvent letGoEvent;

    public float timeToDoubleHit = 0.5f;

    public bool doubleAvailable;
    public void CheckIfDouble()
    {
        if (doubleAvailable)
        {
            //fire the double hit
            doubleHitEvent.Invoke();
            doubleAvailable = false; //so someone cant spam a triple click
            StopAllCoroutines();
        }
    }
    public void StartDoubleTimer()
    {
        if (doubleAvailable){ return; }
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        doubleAvailable = true;
        yield return new WaitForSeconds(timeToDoubleHit);
        Debug.Log("timer done");
        doubleAvailable = false;
        yield return null;
    }
}
