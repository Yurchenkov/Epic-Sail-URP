using UnityEngine;

public class Obstacle : MonoBehaviour {

    private LoseWindow _loseWindow;
    public enum ObstacleTypes {
        Static,
        Floatable,
        Breakable,
        Matchable
    };

    public ObstacleTypes obstacleType;

    private int _safetyMargin;

    private void Awake() {
        _loseWindow = FindObjectOfType<LoseWindow>();

        if (IsBreakable())
            SetSafetyMargin();
    }

    private bool IsBreakable() {
        return obstacleType.ToString().Equals(Constants.OBSTACLE_TYPE_BREAKABLE);
    }

    private void SetSafetyMargin() {
        _safetyMargin = Random.Range(3, 10);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) 
            _loseWindow.OpenLoseWindow(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (IsBreakable() && other.gameObject.CompareTag(Constants.TAG_POINTER)) {
            Damage();
        }
    }

    private void Damage() {
        if (_safetyMargin.Equals(0)) {
            Destroy(gameObject);
            return;
        }

        _safetyMargin--;
    }
}
