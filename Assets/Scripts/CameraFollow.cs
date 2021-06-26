using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform _target;
    private Vector3 _offset;
    private float _smoothTime = 0.3f;
    private Vector3 _velocity = Vector3.zero;
    private GameManager _gameManager;
    private Transform _myTransform;

    private void Awake() {
        _target = GameObject.FindGameObjectWithTag(GameManager.TAG_PLAYER).transform;
        _offset = transform.position - _target.position;
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _myTransform = transform;
    }

    private void LateUpdate() {
        Vector3 targetPosition = _target.position + _offset;
        if (_gameManager.currentLevelType.Equals(GameManager.LEVEL_TYPE_LINEAR))
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f);

        _myTransform.position = Vector3.SmoothDamp(_myTransform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
