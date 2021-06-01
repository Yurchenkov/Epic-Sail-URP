using UnityEngine;

public class CoinController : MonoBehaviour {

    private float _speed = 250f;
    private GameManager _gameManager;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
    }

    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Quaternion rotation = Quaternion.Euler(0, Time.deltaTime * _speed, 0);
        transform.rotation *= rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameManager.TAG_PLAYER)) {
            IncreaseCoinCounter();
            Destroy(gameObject);
        }
    }

    private void IncreaseCoinCounter() {
        _gameManager.IncreaseLocalCoinCounter();
    }
}
