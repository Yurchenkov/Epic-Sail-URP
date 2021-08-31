using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public enum LevelTypes {
        Open,
        Linear,
        Tutorial
    };

    public LevelTypes levelType;

    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    void Update() {
        GameManager.instance.currentLevelType = levelType.ToString();
        if (IsNeedMoveLevel())
            _transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }

    private bool IsNeedMoveLevel() {
        return GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_LINEAR 
            || GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_TUTORIAL 
            && !GameManager.instance.isGamePaused;
    }
}
