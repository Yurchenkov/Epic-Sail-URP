using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

    private Text _coinCounterText;
    private GameManager _gameManager;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _coinCounterText = GameObject.FindGameObjectWithTag(GameManager.TAG_COIN_COUNTER).GetComponent<Text>();
    }

    private void Update() {
        DisplayCoinCount();
    }

    private void DisplayCoinCount() {
        _coinCounterText.text = _gameManager.localCoinCounter.ToString();
    }
}
