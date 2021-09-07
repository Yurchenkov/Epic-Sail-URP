using UnityEngine;

public class Obstacle : MonoBehaviour {

    public AnimationCurve chanceToCreating;
    public int safetyMargin;

    private LoseWindow _loseWindow;

    public enum ObstacleTypes {
        Static,
        Floatable,
        Breakable,
        Matchable,
    };

    public ObstacleTypes obstacleType;

    public enum MatchTypes {
        Type1,
        Type2,
        Type3,
    };

    public MatchTypes matchType; //TODO: make visible only if the Matchable type is selected

    private void Awake() {
        _loseWindow = FindObjectOfType<LoseWindow>();

        if (IsBreakable())
            SetSafetyMargin();
    }

    private bool IsBreakable() {
        return obstacleType.ToString().Equals(Constants.OBSTACLE_TYPE_BREAKABLE);
    }

    private void SetSafetyMargin() {
        safetyMargin = Random.Range(3, 10);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
            _loseWindow.OpenLoseWindow(gameObject);

        if (collision.gameObject.CompareTag(Constants.TAG_OBSTACLE))
            HandleCollision(collision);
    }

    private void HandleCollision(Collision collision) {
        Obstacle collisionObstacleComponent = collision.gameObject.GetComponent<Obstacle>();
        if (IsMatchable() && IsMatchable(collisionObstacleComponent) && IsSameMatchType(collisionObstacleComponent)) {
            RecordTable.AddObstacleCost(2);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private bool IsMatchable() {
        return obstacleType.ToString().Equals(Constants.OBSTACLE_TYPE_MATCHABLE);
    }

    private bool IsMatchable(Obstacle obstacle) {
        return obstacle.obstacleType.ToString().Equals(Constants.OBSTACLE_TYPE_MATCHABLE);
    }

    private bool IsSameMatchType(Obstacle obstacle) {
        return obstacle.matchType.ToString().Equals(matchType.ToString());
    }

    private void OnTriggerEnter(Collider other) {
        if (IsBreakable() && other.gameObject.CompareTag(Constants.TAG_POINTER))
            Damage();
    }

    private void Damage() {
        safetyMargin--;
        if (safetyMargin.Equals(0)) {
            RecordTable.AddObstacleCost();
            Destroy(gameObject);
            return;
        }
    }
}
