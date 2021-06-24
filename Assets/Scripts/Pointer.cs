using UnityEngine;

public class Pointer : MonoBehaviour {

    public Vector3 lastTrailPointerPosition;

    private LayerMask _layerMask;
    private TrailRenderer _trailRenderer;
    private SphereCollider _pointerCollider;
    private GameManager _gameManager;
    private bool _isPushSomething = false;
    private GameObject _pushedObject;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _pointerCollider = GetComponent<SphereCollider>();
        _layerMask = LayerMask.GetMask(GameManager.LAYER_MASK_WATER);
    }

    private void Update() {
        if (_gameManager.isGamePaused || !GameManager.IsTutorialComplete(GameManager.TUTORIAL_TYPE_MOVEMENT))
            return;

        RenderTrail();
        Move();
        FillLastTrailPointerPosition();
    }

    private void RenderTrail() {
        bool mouseInput = Input.GetMouseButton(0);
        _trailRenderer.enabled = mouseInput;
        _pointerCollider.enabled = mouseInput;
    }

    private void Move() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            transform.position = raycastHit.point;
    }

    private void FillLastTrailPointerPosition() {
        if (Input.GetMouseButtonUp(0) && _isPushSomething) {
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

    private void OnTriggerEnter(Collider other) {
        _isPushSomething = true;

        if (_pushedObject == null)
            _pushedObject = other.gameObject;
    }
}
