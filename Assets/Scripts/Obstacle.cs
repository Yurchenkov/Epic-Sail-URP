using UnityEngine;

public class Obstacle : MonoBehaviour {
  
    private GameManager _gameManager;
  
    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(Constants.TAG_GAME_MANAGER).GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) {
            _gameManager.Restart();
        }
    }
}
