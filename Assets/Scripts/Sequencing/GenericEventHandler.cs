using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To be used univerally as
                        an event handler for
                        absolutely any event.
                        I used it for progress
                        on the corkboard and flashlight.
*/
public class GenericEventHandler : MonoBehaviour
{
    public UnityEvent OnAwakeEvent;
    public UnityEvent OnStartEvent;
    public UnityEvent OnDestroyEvent;
    [Tooltip("This is called by another script, make sure that the other script has a reference to this component on the object to call this whenever needed.")]
    public UnityEvent ByScriptEvent;
    private void Awake()
    {
        OnAwakeEvent.Invoke();
    }
    private void Start() {
        OnStartEvent.Invoke();
    }
    private void OnDestroy()
    {
        OnDestroyEvent.Invoke();
    }
}
