using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed2 : MonoBehaviour
{
    public bool key2Destroyed = false;
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
        key2Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
