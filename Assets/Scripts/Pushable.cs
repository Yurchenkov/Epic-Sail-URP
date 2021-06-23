using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Pointer _pointer;

    private bool _isToched = false;
    private Rigidbody _rigidbody;
    private Vector3 _startTrailPosition;

    private void Awake()
    {
        _pointer = GameObject.FindGameObjectWithTag(GameManager.TAG_POINTER).GetComponent<Pointer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        Vector3 target = GetMotionTarget(); // TODO: It should start with event on Pointer
        if (_isToched && target != _startTrailPosition) {
            _rigidbody.AddForce(GetForceVector(target), ForceMode.Impulse);
            _rigidbody.AddTorque(GetDirection(target) * Random.Range(1, 4));
            _isToched = false;
        }
    }
    private Vector3 GetForceVector(Vector3 target)
    {
        Vector3 forceVector = GetDirection(target).normalized;
        forceVector.y = 1f;
        return forceVector * GetDistance(target);
    }
    private Vector3 GetMotionTarget()
    {
        Vector3 target = _pointer.lastTrailPointerPosition;
        return target;
    }

    private float GetDistance(Vector3 target)
    {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target)
    {
        return target - transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        _isToched = true;
    }
}
