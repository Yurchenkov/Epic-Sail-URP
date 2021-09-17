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
        if (GameManager.Instance.currentLevelType == Constants.LEVEL_TYPE_TUTORIAL)
            return;
        RecordTable.AddGroundCost();
        GroundCreator.instance.DeletePiece(gameObject);
    }
}
