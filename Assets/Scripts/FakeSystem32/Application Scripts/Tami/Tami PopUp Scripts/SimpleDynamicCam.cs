using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : Super simple dynamic
                        cam for tami platformer
*/
public class SimpleDynamicCam : MonoBehaviour
{
    [SerializeField] private Transform objToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpTime;
    [SerializeField] private float leftClamp;
    [SerializeField] private float rightClamp;

    private void Update()
    {
        // Lerp to the position + offset, and prevent camera from going off screen.
        float x = objToFollow.position.x + offset.x;
        float y = objToFollow.position.y + offset.y;
        float z = objToFollow.position.z + offset.z;
        x = Mathf.Clamp(x, leftClamp, rightClamp);
        transform.position = Vector3.Slerp(transform.position, new Vector3(x, y, z), Time.deltaTime * lerpTime);
    }
}
