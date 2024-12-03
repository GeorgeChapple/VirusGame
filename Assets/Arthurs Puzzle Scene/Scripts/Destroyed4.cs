using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed4 : MonoBehaviour
{
    public bool key4Destroyed = false;
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
        key4Destroyed = true;
        // WORKING print(key1Destroyed)
    }
}
