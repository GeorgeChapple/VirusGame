using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
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
        float x = objToFollow.position.x + offset.x;
        float y = objToFollow.position.y + offset.y;
        float z = objToFollow.position.z + offset.z;
        x = Mathf.Clamp(x, leftClamp, rightClamp);
        transform.position = Vector3.Slerp(transform.position, new Vector3(x, y, z), Time.deltaTime * lerpTime);
    }
}
