using UnityEngine;

public class Pushable : MonoBehaviour {

    public bool isPushed = false;
    public Vector3 motionTarget;
    public float speed = 1f;

    [SerializeField] private float _defaultPositionY = 0.7f;
    [SerializeField] private float _tilt = 1f;

    private GameManager _gameManager;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
    }

    private void Update() {
        if (!isPushed)
            return;

        Vector3 target = GetMotionTarget();
        Move(target);

        if (CompareTag(GameManager.TAG_PLAYER))
            Rotate(target);
    }

    private Vector3 GetMotionTarget() {
        Vector3 target = motionTarget;
        target.y += _defaultPositionY;
        return target;
    }

    private void Move(Vector3 target) {
        float step = GetStep(target);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private float GetStep(Vector3 target) {
        return (speed * GetDistance(target)) * Time.deltaTime;
    }

    private float GetDistance(Vector3 target) {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target) {
        return target - transform.position;
    }

    private void Rotate(Vector3 target) {
        if (_gameManager.currentLevelType == GameManager.LEVEL_TYPE_OPEN) {
            SetOpenLevelRotation(target);
            return;
        }

        SetLinearLevelRotation(target);
    }

    private void SetOpenLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, direction);
        Quaternion incline = Quaternion.Euler(direction.z * _tilt, 0, -direction.x * _tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, incline * fromToRotation, GetStep(target));
    }

    private void SetLinearLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        transform.rotation = Quaternion.Euler(direction.z, 0, -direction.x);
    }
}
