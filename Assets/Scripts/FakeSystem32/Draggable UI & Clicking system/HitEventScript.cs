using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
    Purpose           : To handle all events that should fire
                        when a user clicks, double clicks, or
                        let's go of the object
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
    // Check if the click should be a double click to fire the doubleHitEvent - Jason
    public void CheckIfDouble()
    {
        if (!IfActive())
        {
            return;
        }
        if (doubleAvailable)
        {
            // Fire the double hit
            doubleHitEvent.Invoke();
            timer = 0;
        }
    }
    // Starts the double timer - Jason
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
    // Simple timer that just stops access to the double click - Jason
    private IEnumerator Timer()
    {
        doubleAvailable = true; // Double click can be done now - Jason
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        doubleAvailable = false;
        yield return null;
    }
    // Ensures its active in the game - Jason
    private bool IfActive()
    {
        if (!gameObject.activeInHierarchy) { return false; }
        return true;
    }
}
