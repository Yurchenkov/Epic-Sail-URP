using UnityEngine;

public class Pushable : MonoBehaviour {

    public bool isPushed = false;
    public Vector3 motionTarget;
    public float speed = 1f;
    public float forceMultiplier = 50f;

    [SerializeField] private float _tilt = 1f;

    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (!isPushed)
            return;

        Move();

        if (CompareTag(Constants.TAG_PLAYER))
            Rotate();
    }

    private void Move() {
        if (!CompareTag(Constants.TAG_PLAYER))
            MoveByForce();

        float step = GetStep();
        motionTarget.y = _transform.position.y;
        _transform.position = Vector3.MoveTowards(_transform.position, motionTarget, step);
    }

    private void MoveByForce() {
        _rigidbody.AddForce(GetForce());
        isPushed = false;
    }

    private Vector3 GetForce() {
        return (motionTarget - _transform.position) * forceMultiplier;
    }

    private float GetStep() {
        return (speed * GetDistance()) * Time.deltaTime;
    }

    private float GetDistance() {
        return GetDirection().magnitude;
    }

    private Vector3 GetDirection() {
        return motionTarget - _transform.position;
    }

    private void Rotate() {
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) {
            SetOpenLevelRotation();
            return;
        }

        SetLinearLevelRotation();
    }

    private void SetOpenLevelRotation() {
        Vector3 direction = GetDirection();
        Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, direction);
        Quaternion incline = Quaternion.Euler(direction.z * _tilt, 0, -direction.x * _tilt);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, incline * fromToRotation, GetStep());
    }

    private void SetLinearLevelRotation() {
        Vector3 direction = GetDirection();
        _transform.rotation = Quaternion.Euler(direction.z, 0, -direction.x);
    }
}
