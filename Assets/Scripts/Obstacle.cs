using UnityEngine;

public class Obstacle : MonoBehaviour {
  
    private GameManager _gameManager;
  
    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
       
        GetComponent<MeshFilter>().mesh = GetComponentInChildren<MeshFilter>().mesh;
        GetComponent<MeshCollider>().sharedMesh = GetComponentInChildren<MeshFilter>().mesh;

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(GameManager.TAG_PLAYER)) {
            _gameManager.Restart();
        }
    }
}
