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
    [SerializeField] private float _angleX, _angleZ;
    private int tiltCount = 3;
    private float tiltCoef =0;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _targetDirection = _transform.rotation.eulerAngles;
        _angleX = _transform.rotation.x;
        _angleZ = transform.rotation.z;
    }

    private void FixedUpdate() {
        //if (CompareTag(Constants.TAG_PLAYER) && GameManager.instance.currentLevelType.Equals(Constants.LEVEL_TYPE_LINEAR))
        //    _rigidbody.velocity = Vector3.right * 5;

        //TiltTo();
        //Tilt();

        if (!isPushed)
            return;

        Move();

        if (CompareTag(Constants.TAG_PLAYER))
            Rotate(motionTarget);

    }
    private void Update() {
        Tilt();
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
        //if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) {
        //    SetOpenLevelRotation(target);
        //    return;
        //}
        //SetLinearLevelRotation(target);
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) RotateBoat();
        FindTiltAngle();
        _isTilt = true;
    }

    //private void SetOpenLevelRotation(Vector3 target) {
    //    Vector3 direction = GetDirection(target);
    //    Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, direction);
    //    Quaternion incline = Quaternion.Euler(direction.z * _tilt, direction.y, -direction.x * _tilt);
    //    _targetDirection = (fromToRotation * incline).eulerAngles;
    //    _isDistabilisation = true;

    //}

    //private void SetLinearLevelRotation(Vector3 target) {
    //    Vector3 direction = GetDirection(target);
    //    _targetDirection = new Vector3(direction.z * _tilt, 0, -direction.x * _tilt);
    //    _isDistabilisation = true;
    //    _isTilt = true;
    //}

    //private void TiltTo() {
    //    Quaternion targetRotation = Quaternion.Euler(_targetDirection);
    //    _transform.rotation = Quaternion.Euler(_transform.rotation.x, targetRotation.y, transform.rotation.z);
    //    if (_transform.rotation != targetRotation && _isDistabilisation) {
    //        _transform.localRotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.Euler(_targetDirection), .35f);
    //    } else if (_isTilt) {
    //        _targetDirection = Vector3.zero;
    //        _isTilt = false;
    //    } else _isDistabilisation = false;
    //}

    private void RotateBoat() {
        Vector3 direction = GetDirection(motionTarget);
        direction.y = 0;
        Quaternion tiltVector = Quaternion.FromToRotation(Vector3.right, direction).normalized;
        _transform.localRotation = tiltVector;
    }

    private void FindTiltAngle() {
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) {
            _angleX = 0;
            _angleZ = Mathf.Clamp((motionStartPoint - motionTarget).magnitude * 3, -20, 20);
        } else {
            _angleX = Mathf.Clamp((motionTarget - motionStartPoint).z * 3, -20, 20);
            _angleZ = Mathf.Clamp((motionStartPoint - motionTarget).x * 3, -20, 20);
        }
        
    }

    private void Tilt() {
        Quaternion rotation = Quaternion.Euler(_angleX, _transform.rotation.eulerAngles.y, _angleZ);
        if (_transform.localRotation != rotation) {
            _transform.localRotation = Quaternion.RotateTowards(_transform.rotation, rotation, 0.5f);
        } else if (tiltCount > 0 && _isTilt) {
            _angleX *= -.8f;
            _angleZ *= -.8f;
            tiltCount--;
        } else if(_isTilt){
            _angleX = 0;
            _angleZ = 0;
            tiltCount = 3;
            _isTilt = false;
            tiltCoef = 0;
        }
        tiltCoef += Time.deltaTime;
    }
}
