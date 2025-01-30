using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSlider : MonoBehaviour
{
    [SerializeField] Transform wholeScene;
    private void Awake()
    {
        //move the thingmabob
        wholeScene.position = new Vector3(Random.Range(50, 200), 0);

    }
}
