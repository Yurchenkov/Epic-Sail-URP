using UnityEngine;

public class Obstacle : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) {
            GameManager.instance.Restart();
        }
    }
}
