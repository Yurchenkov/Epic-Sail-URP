using UnityEngine;

public class Floatable : MonoBehaviour {

    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public Rigidbody rigidBody;

    private void FixedUpdate() {
        if (transform.position.y < 0f) {
            ApplyArchimedeanForce();
        }
    }

    private void ApplyArchimedeanForce() {
        rigidBody.AddForce(CalculateArchimedeanForce(), ForceMode.Acceleration);
    }

    private Vector3 CalculateArchimedeanForce() {
        return new Vector3(0f, Mathf.Abs(Physics.gravity.y) * CalculateDisplacementMultiplier(), 0f);
    }

    private float CalculateDisplacementMultiplier() {
        return Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
    }
}
