using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform _target;
    private Vector3 _offset;
    // private float _smoothTime = 0.3f;
    // private Vector3 _velocity = Vector3.zero;
    private Transform _transform;

    private void Awake() {
        _target = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform;
        _offset = transform.position - _target.position;
        _transform = transform;
    }

    private void LateUpdate() {
        Vector3 targetPosition = _target.position + _offset;
        if (GameManager.instance.currentLevelType.Equals(Constants.LEVEL_TYPE_LINEAR))
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f);

        _transform.position = targetPosition;
    }
}
