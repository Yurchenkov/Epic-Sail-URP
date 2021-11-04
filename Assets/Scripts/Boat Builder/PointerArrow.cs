using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    public float range;
    public float speed;

    private bool isRigthDirection = true;
    private Transform _transform;
    private Vector3 startPosition;

    private void Start() {
        _transform = GetComponent<Transform>();
        startPosition = _transform.position;
    }
    void Update()
    {
        ChangeDirection();
        MoveArrow();
    }

    private void MoveArrow() {
        Vector3 newPosition = _transform.position;
        if (isRigthDirection)
            newPosition.x += speed * Time.deltaTime;
        else
            newPosition.x -= speed * Time.deltaTime;
        _transform.position = newPosition;
    }

    private void ChangeDirection() {
        if (_transform.position.x >= startPosition.x + range)
            isRigthDirection = false;
        if (_transform.position.x <= startPosition.x)
            isRigthDirection = true;
    }
}
