using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/
[CreateAssetMenu(fileName = "Event")]
public class EventPass : ScriptableObject
{
    public bool passValThrough;
    public string methodName;

    public bool passSelfVal;
    public FileData self;

    public bool passStringVal;
    public string stringVal;

    public bool passIntVal;
    public int intVal;

    public bool passFloatVal;
    public float floatVal;
}
