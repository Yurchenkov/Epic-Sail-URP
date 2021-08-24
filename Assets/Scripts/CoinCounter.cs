using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

    private Text _coinCounterText;

    private void Awake() {
        _coinCounterText = GameObject.FindGameObjectWithTag(Constants.TAG_COIN_COUNTER).GetComponent<Text>();
    }

    private void Update() {
        DisplayCoinCount();
    }

    private void DisplayCoinCount() {
        _coinCounterText.text = GameManager.instance.playerData.levelMoney.ToString() != null ? GameManager.instance.playerData.levelMoney.ToString() : "";
    }
}
