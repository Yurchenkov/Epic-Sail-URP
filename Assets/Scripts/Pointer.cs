using UnityEngine;

public class Pointer : MonoBehaviour {

    public Vector3 lastTrailPointerPosition;

    private LayerMask _layerMask;
    private TrailRenderer _trailRenderer;
    private SphereCollider _pointerCollider;
    private GameManager _gameManager;
    private bool _isPushSomething = false;
    private GameObject _pushedObject;
    private Camera _mainCamera;
    private Vector3 _inputPosition;
    private bool _isTouched;
    private bool _isEndingTouch;
    private Transform _myTransform;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _pointerCollider = GetComponent<SphereCollider>();
        _layerMask = LayerMask.GetMask(GameManager.LAYER_MASK_WATER);
        _mainCamera = Camera.main;
        _myTransform = transform;
    }

    private void Update() {
#if UNITY_EDITOR
        GameManager.CompleteTutorial(GameManager.TUTORIAL_TYPE_MOVEMENT); // TODO: is used in development mode. Remove before release
#endif
        if (_gameManager.isGamePaused || !GameManager.IsTutorialComplete(GameManager.TUTORIAL_TYPE_MOVEMENT))
            return;

        CheckInput();
        RenderTrail();
        Move();
        FillLastTrailPointerPosition();
    }

    private void RenderTrail() {
        bool mouseInput = _isTouched;
        _trailRenderer.enabled = mouseInput;
        _pointerCollider.enabled = mouseInput;
    }

    private void Move() {
        Ray ray = _mainCamera.ScreenPointToRay(_inputPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            _myTransform.position = raycastHit.point;
    }

    private void FillLastTrailPointerPosition() {
        if (_isEndingTouch && _isPushSomething) {
            lastTrailPointerPosition = _myTransform.position;

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
            _isEndingTouch = touch.phase == TouchPhase.Ended;
            return;
        }
#if UNITY_EDITOR
        _inputPosition = Input.mousePosition;
        _isTouched = Input.GetMouseButton(0);
        _isEndingTouch = Input.GetMouseButtonUp(0);
#endif
    }

    private void OnTriggerEnter(Collider other) {
        _isPushSomething = true;

        if (_pushedObject == null)
            _pushedObject = other.gameObject;
    }
}
