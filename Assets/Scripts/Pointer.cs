using UnityEngine;

public class Pointer : MonoBehaviour {

    public Vector3 lastTrailPointerPosition;

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
        GameManager.CompleteTutorial(Constants.TUTORIAL_TYPE_MOVEMENT); // TODO: is used in development mode. Remove before release
#endif
        if (GameManager.instance.isGamePaused || !GameManager.IsTutorialComplete(Constants.TUTORIAL_TYPE_MOVEMENT))
            return;

        CheckInput();
        RenderTrail();
#if UNITY_EDITOR // TODO: is used in development mode. Remove before release
        _isTouched = true;
#endif
        if (_isTouched) {
            Move();
            FillLastTrailPointerPosition();
        }
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

    private void FillLastTrailPointerPosition() {
        if (_isTouchEnded && _isPushSomething) {
            lastTrailPointerPosition = _transform.position;

            Pushable pushableComponent = _pushedObject.GetComponent<Pushable>();
            if (pushableComponent == null) {
                _isPushSomething = false;
                _pushedObject = null;
                return;
            }

            pushableComponent.isPushed = _isPushSomething;
            pushableComponent.motionTarget = lastTrailPointerPosition;

            _isPushSomething = false;
            _pushedObject = null;
        }
    }

    private void CheckInput() { 
        _isTouched = Input.touchCount > 0;
        if (_isTouched) {
            Touch touch = Input.GetTouch(0);
            _inputPosition = touch.position;
            _isTouchEnded = touch.phase == TouchPhase.Ended;
            return;
        }
#if UNITY_EDITOR
        _inputPosition = Input.mousePosition;
        _isTouched = Input.GetMouseButton(0);
        _isTouchEnded = Input.GetMouseButtonUp(0);
#endif
    }

    private void OnTriggerEnter(Collider other) {
        _isPushSomething = true;

        if (_pushedObject == null)
            _pushedObject = other.gameObject;
    }
}
