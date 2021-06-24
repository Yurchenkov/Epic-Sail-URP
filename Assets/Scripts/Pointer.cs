using UnityEngine;


public class Pointer : MonoBehaviour {

    public Vector3 direction;
    public bool takeable;
    public bool isPushSomething = false;

    [SerializeField] private System.Collections.Generic.List<Pushable> _pushableObjectsList;

    private LayerMask _layerMask;
    private TrailRenderer _trailRenderer;
    private GameManager _gameManager;
    private Transform _myTransform;
    private bool _isDraging;
    private Vector3 _startTrailPointerPosition;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.enabled = false;
        _myTransform = GetComponent<Transform>();
        _layerMask = LayerMask.GetMask(GameManager.LAYER_MASK_WATER);
    }

    private void Update() {
        if (_gameManager.isGamePaused)
            return;
        Move();
    }

    private void Move() {
        Ray ray;
        if (Input.touchCount>0) {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        } else {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
          
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask)) {
            _myTransform.position = raycastHit.point;
        }     
    }
       
    private void OnMouseDown() {
        takeable = false;
        _startTrailPointerPosition = _myTransform.position;
        _trailRenderer.enabled = true;
        _isDraging = true;
        isPushSomething = false;
    }

    private void OnMouseUp() {
        direction = _myTransform.position - _startTrailPointerPosition;
        _trailRenderer.enabled = false;
        takeable = true;
        _isDraging = false;
        PushTochingObjects();
    }

    private void OnMouseDrag() {
        takeable = false;
    }

    private void PushTochingObjects() {
        if (_pushableObjectsList.Count>0) {
            foreach (Pushable pushableObject in _pushableObjectsList) {
                pushableObject.direction = direction;
                pushableObject.isToched = true;
            }
            _pushableObjectsList.Clear();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (_isDraging) {
            isPushSomething = true;
            Pushable pushableObject = other.gameObject.GetComponentInParent<Pushable>();
            if (pushableObject) {
                if (!pushableObject.isAdded) {
                    _pushableObjectsList.Add(pushableObject);
                    pushableObject.isAdded = true;
                }
            }
        }
    }
}
