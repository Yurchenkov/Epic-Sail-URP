using UnityEngine;

public class CoinController : MonoBehaviour {

    private float _speed = 250f;
    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Quaternion rotation = Quaternion.Euler(0, Time.deltaTime * _speed, 0);
        _transform.rotation *= rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER)) {
            IncreaseCoinCounter();
            Destroy(gameObject);
        }
    }

    private void IncreaseCoinCounter() {
        GameManager.instance.IncreaseLocalCoinCounter();
    }

    private void OnBecameInvisible() {
        if (GameManager.instance.currentLevelType != Constants.LEVEL_TYPE_OPEN) Destroy(gameObject);        
    }
}
