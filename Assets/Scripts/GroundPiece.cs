using UnityEngine;

public class GroundPiece : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;
    public float scaleX = .6f;

    private void Awake() {
        scaleX = transform.localScale.x;
    }

    private void OnBecameInvisible() {
        GroundCreator.instance.deletePiece(gameObject);
    }
}
