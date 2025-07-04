using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
*/
public class SpriteHandlerScript : MonoBehaviour {
    public Sprite[] spriteSheet;
    public int spriteIndex;
    [SerializeField] private Image image;

    // Get image component of object, set sprite to default index
    public void SetUp() {
        //image = GetComponent<Image>();
        if (image == null)
        {
            TryGetComponent<Image>(out Image component);
            image = component;
        }
        RefreshSprite();
    }

    // Refresh sprite to the currently set index
    public void RefreshSprite() {
        if (spriteIndex >= spriteSheet.Length) {
            spriteIndex = 0;
            Debug.Log("Sprite Index out of bounds, reset to 0");
        } else if (spriteIndex < 0) {
            spriteIndex = spriteSheet.Length - 1;
            Debug.Log("Sprite Index out of bounds, reset to " + (spriteSheet.Length - 1));
        }
        image.sprite = spriteSheet[spriteIndex];
    }
    public void ReceiveSprites(FileData fileData) // Get Sprites from filedata - Jason
    {
        spriteSheet = fileData.icon.ToArray();
    }

    public void SetSpriteIndex(int newIndex) {
        spriteIndex = newIndex;
        RefreshSprite();
    }
}