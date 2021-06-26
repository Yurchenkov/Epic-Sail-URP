using UnityEngine;

public class Pointer : MonoBehaviour {

    public Vector3 lastTrailPointerPosition;

    private LayerMask _layerMask;
    private TrailRenderer _trailRenderer;
    private SphereCollider _pointerCollider;
    private GameManager _gameManager;
    private bool _isPushSomething = false;
    private GameObject _pushedObject;

    private Vector3 _iputPosition;
    private bool _getTouch;
    private bool _getTouchUp;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _pointerCollider = GetComponent<SphereCollider>();
        _layerMask = LayerMask.GetMask(GameManager.LAYER_MASK_WATER);
    }

    private void Update() {
        GameManager.CompleteTutorial("Movement");
        if (_gameManager.isGamePaused || !GameManager.IsTutorialComplete(GameManager.TUTORIAL_TYPE_MOVEMENT))
            return;

        CheckInput();
        RenderTrail();
        Move();
        FillLastTrailPointerPosition();
    }

    private void RenderTrail() {
        bool mouseInput = _getTouch;
        _trailRenderer.enabled = mouseInput;
        _pointerCollider.enabled = mouseInput;
    }

    private void Move() {
        Ray ray = Camera.main.ScreenPointToRay(_iputPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            transform.position = raycastHit.point;
    }

    private void FillLastTrailPointerPosition() {
        if (_getTouchUp && _isPushSomething) {
            lastTrailPointerPosition = transform.position;

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
        _getTouchUp = false;
        if (Input.touchCount>0) {
            _iputPosition = Input.GetTouch(0).position;
            _getTouch = true;
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                _getTouchUp = true;
            }
        } else if (Input.GetMouseButton(0)) {
            _iputPosition = Input.mousePosition;
            _getTouch = true;
        } else {
            _getTouch = false;
        }
        if (Input.GetMouseButtonUp(0)) {
            _getTouchUp = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        _isPushSomething = true;

        if (_pushedObject == null)
            _pushedObject = other.gameObject;
    }
}
