using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class SpriteHandlerScript : MonoBehaviour {
    [SerializeField] private Sprite[] spriteSheet;
    public int spriteIndex;
    private Image image;

    // Get image component of object, set sprite to default index
    private void Awake() {
        image = GetComponent<Image>();
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
}