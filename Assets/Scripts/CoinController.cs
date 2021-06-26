using UnityEngine;

public class CoinController : MonoBehaviour {

    private float _speed = 250f;
    private GameManager _gameManager;
    private Transform _myTransform;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _myTransform = transform;
    }

    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Quaternion rotation = Quaternion.Euler(0, Time.deltaTime * _speed, 0);
        _myTransform.rotation *= rotation;
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
