using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed3 : MonoBehaviour
{
    public bool key3Destroyed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        key3Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
