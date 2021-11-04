using UnityEngine;

public class RayGun : MonoBehaviour {

    public static RayGun Instance { get; private set; }

    public delegate void RayShoting(Vector2 direction, bool hasTarget);
    public event RayShoting ShotingIsFinished;

    [SerializeField] private LayerMask _layerMask;

    private Transform _transform;
    private Camera _camera;
    private Touch _movingTouch;
    private bool _isTouch;
    private bool _hasStart;
    private Vector2 _startPosition;
    private Vector2 _inputPosition = Vector2.zero;
    private Vector2 _endPosition = Vector2.zero;
    private bool _isTouchEnded = false;
    private bool _hasTarget = false;

    private void Awake() {
        Instance = this;
        _transform = transform;
        _camera = Camera.main;
    }

    private void Update() {
        //_isTouch = Input.touchCount > 0;
        //if (_isTouch && !_hasStart) {
        //    _movingTouch = Input.GetTouch(0);
        //    GetPosition();
        //}
        //_endPosition = _movingTouch.position;
        //if (_movingTouch.phase == TouchPhase.Ended) {
        //    _hasStart = false;
        //}
        CalculateInput();
        if (_isTouch) {
            if (!_hasStart) {
                //_movingTouch = Input.GetTouch(0);
                SetStartPosition();
                Debug.Log(_inputPosition);
            }
            //_endPosition = _inputPosition;
            ShootRay();
        } else {
            _hasStart = false;
        }
        if(_isTouchEnded) {
            Vector2 result = _endPosition - _startPosition;
            Vector2 direction = new Vector2(result.x / _camera.pixelWidth, Mathf.Clamp(result.y / _camera.pixelWidth, -1, 1));
            ShotingIsFinished?.Invoke(direction, _hasTarget);
            _hasTarget = false;
        }
    }

    private void SetStartPosition() {
        _startPosition = _inputPosition;
        _hasStart = true;
    }

    private void ShootRay() {
        Ray ray = _camera.ScreenPointToRay(_inputPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask)) {
            raycastHit.transform.gameObject.GetComponent<MovingSystem>().MakeTarget();
            _hasTarget = true;
            Debug.Log("got target");
        }
    }

    private void CalculateInput() {
        _isTouch = Input.touchCount > 0;
        if (_isTouch) {
            _movingTouch = Input.GetTouch(0);
            _inputPosition = _movingTouch.position;
            _isTouchEnded = _movingTouch.phase == TouchPhase.Ended;
            _endPosition = _isTouchEnded ? _movingTouch.position : _endPosition;
            return;
        }

#if UNITY_EDITOR
        _inputPosition = Input.mousePosition;
        _isTouch = Input.GetMouseButton(0);
        _isTouchEnded = Input.GetMouseButtonUp(0);
        _endPosition = _isTouchEnded ? _inputPosition : _endPosition;
#endif
    }
}
