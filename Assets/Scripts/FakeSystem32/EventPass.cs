using UnityEngine;
[CreateAssetMenu(fileName = "Event")]
public class EventPass : ScriptableObject
{
    public string methodName;
    public int intVal;
    public float floatVal;
    public bool passValThrough;
}
