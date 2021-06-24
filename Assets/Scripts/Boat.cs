using UnityEngine;

public class Boat : MonoBehaviour {

    public float speed = 1f;

    [SerializeField] private float _defaultPositionY = 0.7f;
    [SerializeField] private float _tilt = 1f;

    private GameManager _gameManager;
    private Pointer _pointer;
    private bool _isToched;
    private bool _isNotTakeDirection = true;
    private Vector3 _direction;
    private Vector3 _target;
    private Transform _myTransform;
    
    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _pointer = GameObject.FindGameObjectWithTag(GameManager.TAG_POINTER).GetComponent<Pointer>();
        _myTransform = GetComponent<Transform>();
        _target = _myTransform.position;
    }

    private void Update() {
        if (_pointer.takeable & (_isToched == _pointer.isPushSomething) & _isNotTakeDirection) {
            _direction = _pointer.direction;
            _target = _myTransform.position + _direction;
            _target.y = _defaultPositionY;
            _isNotTakeDirection = false;
            _isToched = false;
        }
        if (!_pointer.takeable) {
            _isNotTakeDirection = true;
        }
        
        Move(_target);
        Rotate(_target);
    }

    private void Move(Vector3 target) {
        float step = GetStep(target);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private float GetStep(Vector3 target) {
        return (speed * GetDistance(target)) * Time.deltaTime;
    }

    private float GetDistance(Vector3 target) {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target) {
        return target - _myTransform.position;
    }

    private void Rotate(Vector3 target) {
        if (_gameManager.currentLevelType == GameManager.LEVEL_TYPE_OPEN) {
            SetOpenLevelRotation(target);
            return;
        }
        SetLinearLevelRotation(target);
    }

    private void SetOpenLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        Quaternion fromToRotation = Quaternion.FromToRotation(Vector3.right, direction);
        Quaternion incline = Quaternion.Euler(direction.z * _tilt, 0, -direction.x * _tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, incline * fromToRotation, GetStep(target));
    }

    private void SetLinearLevelRotation(Vector3 target) {
        Vector3 direction = GetDirection(target);
        transform.rotation = Quaternion.Euler(direction.z, 0, -direction.x);
    }

    private void OnTriggerEnter(Collider other) {
        _isToched = true;
    }
}
