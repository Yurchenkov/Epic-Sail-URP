using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

    private Text _coinCounterText;

    private void Awake() {
        _coinCounterText = GameObject.FindGameObjectWithTag(Constants.TAG_COIN_COUNTER_TEXT).GetComponent<Text>();
    }

    public void IncreaseCoinCounter() {
        GameManager.Instance.playerData.AddMoney();
        DisplayCoinCount();
    }

    private void DisplayCoinCount() {
        _coinCounterText.text = GameManager.Instance.playerData.levelMoney.ToString() ?? "";
    }
}
