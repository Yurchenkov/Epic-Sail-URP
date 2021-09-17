using UnityEngine;

public class Pointer : MonoBehaviour {

    private Vector3 _endPoint;
    private LayerMask _layerMask;
    private TrailRenderer _trailRenderer;
    private SphereCollider _pointerCollider;
    private bool _isPushSomething = false;
    private GameObject _pushedObject;
    private Camera _mainCamera;
    private Vector3 _inputPosition;
    private bool _isTouched;
    private bool _isTouchEnded;
    private Transform _transform;

    private void Awake() {
        _trailRenderer = GetComponent<TrailRenderer>();
        _pointerCollider = GetComponent<SphereCollider>();
        _layerMask = LayerMask.GetMask(Constants.LAYER_MASK_WATER);
        _mainCamera = Camera.main;
        _transform = transform;
    }

    private void Update() {
#if UNITY_EDITOR
        GameManager.Instance.playerData.CompleteTutorial(Constants.TUTORIAL_TYPE_MOVEMENT); // TODO: is used in development mode. Remove before release
#endif
        if (GameManager.Instance.isGamePaused || !GameManager.Instance.playerData.IsTutorialComplete(Constants.TUTORIAL_TYPE_MOVEMENT))
            return;

        CalculateInput();
        RenderTrail();

#if UNITY_EDITOR // TODO: is used in development mode. Remove before release
        _isTouched = true;
#endif

        if (_isTouched) {
            Move();
            SetPushableObjectParams();
        }
    }

    private void CalculateInput() {
        _isTouched = Input.touchCount > 0;
        if (_isTouched) {
            Touch touch = Input.GetTouch(0);
            _inputPosition = touch.position;
            _isTouchEnded = touch.phase == TouchPhase.Ended;
            _endPoint = _isTouchEnded ? _transform.position : _endPoint;
            return;
        }

#if UNITY_EDITOR
        _inputPosition = Input.mousePosition;
        _isTouched = Input.GetMouseButton(0);
        _isTouchEnded = Input.GetMouseButtonUp(0);
        _endPoint = _isTouchEnded ? _transform.position : _endPoint;
#endif
    }

    private void RenderTrail() {
        bool mouseInput = _isTouched;
        _trailRenderer.enabled = mouseInput;
        _pointerCollider.enabled = mouseInput;
    }

    private void Move() {
        Ray ray = _mainCamera.ScreenPointToRay(_inputPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            _transform.position = raycastHit.point;
    }

    private void SetPushableObjectParams() {
        if (_isTouchEnded && _isPushSomething && _pushedObject) {
            Pushable pushableComponent = _pushedObject.GetComponent<Pushable>();
            if (pushableComponent == null) {
                _isPushSomething = false;
                _pushedObject = null;
                return;
            }

            pushableComponent.isPushed = _isPushSomething;
            pushableComponent.motionTarget = _endPoint;

            _isPushSomething = false;
            _pushedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        _isPushSomething = true;

        if (_pushedObject == null)
            _pushedObject = other.gameObject;
    }
}
