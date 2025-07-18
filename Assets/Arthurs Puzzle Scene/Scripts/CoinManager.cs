using UnityEngine;
using TMPro;

/*
    Script created by : Arthur Wakeman
    Edited by         : Arthur Wakeman
*/

public class CoinManager : MonoBehaviour {
    public static CoinManager instance;
    private int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }

    private void OnGUI() {
        coinsDisplay.text = coins.ToString();

    }

    public void ChangeCoins(int amount) {
        coins += amount;
    }
}
