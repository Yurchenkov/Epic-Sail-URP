using UnityEngine;

public class Pushable : MonoBehaviour {
    public bool isToched = false;
    public Vector3 direction;
    public bool isAdded;

    private Rigidbody _rigidbody;


    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        Vector3 target = direction;

        if (isToched) {
            _rigidbody.AddForce(GetForceVector(target), ForceMode.Impulse);
            _rigidbody.AddTorque(GetDirection(target) * Random.Range(1, 4));
            isToched = false;
            isAdded = false;
        }
    }

    private Vector3 GetForceVector(Vector3 target) {
        Vector3 forceVector = GetDirection(target).normalized;
        forceVector.y = 1f;
        return forceVector * GetDistance(target);
    }
   
    private float GetDistance(Vector3 target) {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target) {
        return target - transform.position;
    }
}
