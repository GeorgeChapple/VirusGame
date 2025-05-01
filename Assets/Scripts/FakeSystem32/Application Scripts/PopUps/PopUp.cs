using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To go on the pop up window and randomise the image on it
*/
public class PopUp : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image popUpImage;

    void Start()
    {
        popUpImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }

}
