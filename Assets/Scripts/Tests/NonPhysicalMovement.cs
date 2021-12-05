using UnityEngine;

public class NonPhysicalMovement : MonoBehaviour
{
    [SerializeField] private bool _isTarget = false;
    [SerializeField] private float maxDirection = 10f;
    //[SerializeField] private float _speed = 10f;
    [SerializeField] private AnimationCurve _speed;
    [SerializeField] private float _maxSpeed = 10f;
   
    private Transform _transform;

    [SerializeField] private float _coveredDistance = 0f;
    [SerializeField] private float _targetDistance = 0f;
    [SerializeField] private float _maxDistance = 5f;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _lastPosition;

    private void Start() {
        _transform = transform;
        if (CompareTag(Constants.TAG_PLAYER))
            RayGun.Instance.ShotingIsFinished += StartMove;
    }

    public void MakeTarget() {
        if (!CompareTag(Constants.TAG_PLAYER))
            RayGun.Instance.ShotingIsFinished += StartMove;
        _isTarget = true;
    }


    private void FixedUpdate() {
        if (_coveredDistance < _targetDistance) {
            moveBoat();
        } else {
            _coveredDistance = 0f;
            _targetDistance = 0f;
            //_rigidbody.AddForce(Vector3.zero);
        }
    }

    private void StartMove(Vector2 direction, bool hasTarget) {
        if (_isTarget || (CompareTag(Constants.TAG_PLAYER) && !hasTarget)) {
            //_rigidbody.AddForce(new Vector3(direction.y, 0, -direction.x) * maxDirection, ForceMode.Acceleration);
            _direction = new Vector3(direction.y, 0, -direction.x).normalized;
            _targetDistance = _maxDistance * direction.magnitude;
            _lastPosition = _transform.position;
            if (!CompareTag(Constants.TAG_PLAYER))
                RayGun.Instance.ShotingIsFinished -= StartMove;
            _isTarget = false;
        }
    }

    private void moveBoat() {
        //_rigidbody.AddForce(_direction * _speed, ForceMode.Force);
        _transform.Translate(_direction *_maxSpeed * _speed.Evaluate(FindSpeedPhase()) * Time.fixedDeltaTime, Space.Self);
        Vector3 movingDistance = _transform.position - _lastPosition;
        _lastPosition = _transform.position;
        _coveredDistance += Mathf.Sqrt(Mathf.Pow(movingDistance.x, 2) + Mathf.Pow(movingDistance.z, 2));
    }

    private float FindSpeedPhase() {
        return 1 - (_targetDistance - _coveredDistance) / _targetDistance;
    }

    private void OnDestroy() {
        RayGun.Instance.ShotingIsFinished -= StartMove;
    }
}
