using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public enum LevelTypes {
        Open,
        Linear
    };

    public LevelTypes levelType;

    private GameManager _gameManager;
    private Transform _transform;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(Constants.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _gameManager.currentLevelType = levelType.ToString();
        _transform = transform;
    }

    void Update() {
        if (_gameManager.currentLevelType == Constants.LEVEL_TYPE_LINEAR)
            _transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}
