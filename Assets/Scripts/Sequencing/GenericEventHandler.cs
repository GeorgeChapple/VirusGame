using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEventHandler : MonoBehaviour
{
    public UnityEvent OnAwakeEvent;
    public UnityEvent OnDestroyEvent;
    [Tooltip("This is called by another script, make sure that the other script has a reference to this component on the object to call this whenever needed.")]
    public UnityEvent ByScriptEvent;
    private void Awake()
    {
        OnAwakeEvent.Invoke();
    }
    private void OnDestroy()
    {
        OnDestroyEvent.Invoke();
    }
}
