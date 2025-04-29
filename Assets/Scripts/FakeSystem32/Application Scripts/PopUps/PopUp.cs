using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image popUpImage;

    void Start()
    {
        popUpImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }

}
