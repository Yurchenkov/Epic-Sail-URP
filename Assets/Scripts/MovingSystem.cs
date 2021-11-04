using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingSystem : MonoBehaviour{

    [SerializeField] private LayerMask _layerMask = 6;
    [SerializeField] private bool _isTarget = false;
    private float maxDirection = 10f;

    private float _raycastDistance = 5f;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _minBorderX;
    private float _maxBorderX;
    private float _movingDistance;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        if(CompareTag(Constants.TAG_PLAYER))
            RayGun.Instance.ShotingIsFinished += StartMove;
    }

    public void MakeTarget() {
        if(!CompareTag(Constants.TAG_PLAYER))
            RayGun.Instance.ShotingIsFinished += StartMove;
        _isTarget = true;
    }

    private void StartMove(Vector2 direction, bool hasTarget) {
        if (_isTarget || (CompareTag(Constants.TAG_PLAYER) && !hasTarget)) {
            _rigidbody.AddForce(new Vector3(direction.y, 0, -direction.x) * maxDirection, ForceMode.Impulse);
            if (!CompareTag(Constants.TAG_PLAYER))
                RayGun.Instance.ShotingIsFinished -= StartMove;
        }
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
        if (other.CompareTag(Constants.LAYER_MASK_WATER)) {
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
