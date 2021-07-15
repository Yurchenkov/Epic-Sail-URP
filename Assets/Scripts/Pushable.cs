using UnityEngine;

public class Pushable : MonoBehaviour {

    public bool isPushed = false;
    public Vector3 motionStartPoint;
    public Vector3 motionTarget;
    public float speed = 1f;
    public float forceMultiplier = 50f;

    [SerializeField] private float _tilt = 1f;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private bool _isDistabilisation = false;
    private bool _isTilt = false;
    private Vector3 _targetDirection;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _targetDirection = _transform.rotation.eulerAngles;
    }

    private void FixedUpdate() {
        //if (CompareTag(Constants.TAG_PLAYER) && GameManager.instance.currentLevelType.Equals(Constants.LEVEL_TYPE_LINEAR))
        //    _rigidbody.velocity = Vector3.right * 5;

        TiltTo();

        if (!isPushed)
            return;

        Move();

        if (CompareTag(Constants.TAG_PLAYER))
            Rotate(motionTarget);

    }

    private void Move() {
        _rigidbody.AddForce(GetForce());
        isPushed = false;
    }

    private Vector3 GetForce() {
        return (motionTarget - motionStartPoint) * forceMultiplier;
    }

    private float GetStep(Vector3 target) {
        return (speed * GetDistance(target)) * Time.deltaTime;
    }

    private float GetDistance(Vector3 target) {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target) {
        return target - _transform.position;
    }

    private void Rotate(Vector3 target) {
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) {
            SetOpenLevelRotation(target);
            return;
        }
        SetLinearLevelRotation(target);
    }

    private void SetOpenLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, direction);
        Quaternion incline = Quaternion.Euler(direction.z * _tilt, 0, -direction.x * _tilt);
        _targetDirection = (fromToRotation * incline).eulerAngles;
        _isDistabilisation = true;
        
    }

    private void SetLinearLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        _targetDirection = new Vector3(direction.z * _tilt, 0, -direction.x * _tilt);
        _isDistabilisation = true;
        _isTilt = true;
    }

    private void TiltTo() {
        Quaternion targetRotation = Quaternion.Euler(_targetDirection);
        if (_transform.rotation != targetRotation && _isDistabilisation) {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.Euler(_targetDirection), .35f);
        } else if (_isTilt) {
            _targetDirection = Vector3.zero;
            _isTilt = false;
        } else _isDistabilisation = false;
    }
}
