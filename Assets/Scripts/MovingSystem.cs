using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingSystem : MonoBehaviour {

    // [SerializeField] private LayerMask _layerMask = 6;
    [SerializeField] private bool _isTarget = false;
    [SerializeField] private float maxDirection = 10f;
    [SerializeField] private float _speed = 1f;

    private float _raycastDistance = 5f;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _minBorderX;
    private float _maxBorderX;
    private float _movingDistance;

    [SerializeField] private float _coveredDistance = 0f;
    [SerializeField] private float _targetDistance = 0f;
    [SerializeField] private float _maxDistance = 5f;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _lastPosition;


    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
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
            _rigidbody.AddForce(Vector3.zero);
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
        _rigidbody.AddForce(_direction * _speed, ForceMode.Force);
        Vector3 movingDistance = _transform.position - _lastPosition;
        _lastPosition = _transform.position;
        _coveredDistance += Mathf.Sqrt(Mathf.Pow(movingDistance.x, 2) + Mathf.Pow(movingDistance.z, 2));
    }


    //private IEnumerator CheckWaterArea() {
    //    while (true) {
    //        Transform waterArea;
    //        RaycastHit[] downPieces;
    //        downPieces = Physics.RaycastAll(_transform.position, Vector3.down, _raycastDistance);
    //        foreach (RaycastHit hit in downPieces) {
    //            GroundPiece groundChecking = hit.transform.gameObject.GetComponent<GroundPiece>();
    //            if (groundChecking != null) {
    //                waterArea = groundChecking.waterArea;
    //                Bounds boundsArea = waterArea.GetComponent<Renderer>().bounds;
    //                _minBorderZ = (boundsArea.center.z - boundsArea.extents.z) * 0.5f;
    //                _maxBorderZ = (boundsArea.center.z + boundsArea.extents.z) * 0.5f;
    //                break;
    //            }
    //        }
    //        yield return new WaitForSeconds(.5f);
    //    }
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 4) {
            Bounds boundsArea = other.GetComponent<Renderer>().bounds;
            _minBorderX = (boundsArea.center.x - boundsArea.extents.x);
            _maxBorderX = (boundsArea.center.x + boundsArea.extents.x);
        }
    }

    private float GetMovingDistance() {
        float distanceToBord = Mathf.Min(_transform.position.x - _minBorderX, _maxBorderX - _transform.position.x);
        return 0f;
    }

    private void OnDestroy() {
        RayGun.Instance.ShotingIsFinished -= StartMove;
    }
}
