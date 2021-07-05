using UnityEngine;

public class Obstacle : MonoBehaviour {

    private LoseWindow _loseWindow;

    private void Awake() {
        _loseWindow = GameObject.FindObjectOfType<LoseWindow>();
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) _loseWindow.OpenLoseWindow(this.gameObject);
    }
}
