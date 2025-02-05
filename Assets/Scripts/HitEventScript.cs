using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
        }
    }
    public void StartDoubleTimer()
    {
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
