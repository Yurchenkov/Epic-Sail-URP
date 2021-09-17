using System.Collections;
using UnityEngine;

public class Pushable : MonoBehaviour {

    public bool isPushed = false;
    public Vector3 motionTarget;
    public float speed = 1f;
    public float forceMultiplier = 50f;

    [SerializeField] private float _tilt = 1f;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _raycastDistance = 5f;
    private float _minBorderZ = 0;
    private float _maxBorderZ = 0;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        if (CompareTag(Constants.TAG_PLAYER))
            StartCoroutine(CheckWaterArea());
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
        else {
            float step = GetStep();
            motionTarget.y = _transform.position.y;
            Vector3 moveTowards = Vector3.MoveTowards(_transform.position, motionTarget, step);
            moveTowards.z = Mathf.Clamp(moveTowards.z, _minBorderZ, _maxBorderZ);
            _transform.position = moveTowards;
        }
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
        if (GameManager.Instance.currentLevelType == Constants.LEVEL_TYPE_OPEN) {
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

    private IEnumerator CheckWaterArea() {
        while (true) {
            Transform waterArea;
            RaycastHit[] downPieces;
            downPieces = Physics.RaycastAll(_transform.position, Vector3.down, _raycastDistance);
            foreach (RaycastHit hit in downPieces) {
                GroundPiece groundChecking = hit.transform.gameObject.GetComponent<GroundPiece>();
                if (groundChecking != null) {
                    waterArea = groundChecking.waterArea;
                    Bounds boundsArea = waterArea.GetComponent<Renderer>().bounds;
                    _minBorderZ = (boundsArea.center.z - boundsArea.extents.z) * 0.5f;
                    _maxBorderZ = (boundsArea.center.z + boundsArea.extents.z) * 0.5f;
                    break;
                }
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}