using UnityEngine;

public class GroundPiece : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;
    public float scaleX = .6f;
    public Transform waterArea;

    private void Awake() {
        scaleX = transform.localScale.x;
    }

    private void OnBecameInvisible() {
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_LINEAR)
            return;

        GroundCreator.instance.DeletePiece(gameObject);
    }
}
