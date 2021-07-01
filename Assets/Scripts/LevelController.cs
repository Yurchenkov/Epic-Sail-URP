using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public enum LevelTypes {
        Open,
        Linear
    };

    public LevelTypes levelType;

    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    void Update() {
        GameManager.instance.currentLevelType = levelType.ToString();
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_LINEAR && !GameManager.instance.isGamePaused)
            _transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}
