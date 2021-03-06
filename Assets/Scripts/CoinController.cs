using UnityEngine;

public class CoinController : MonoBehaviour {

    private float _rotationSpeed = 250f;
    private Transform _transform;
    private CoinCounter _coinCounter;

    private void Awake() {
        _transform = transform;
        _coinCounter = GameObject.FindGameObjectWithTag(Constants.TAG_COIN_COUNTER).GetComponent<CoinCounter>(); ;
    }

    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Quaternion rotation = Quaternion.Euler(0, Time.time * _rotationSpeed, 0);
        _transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER)) {
            _coinCounter.IncreaseCoinCounter();
            RecordTable.AddCoinCost();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        if (!GameManager.Instance.currentLevelType.Equals(Constants.LEVEL_TYPE_OPEN))
            Destroy(gameObject);
    }
}
