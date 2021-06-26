using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public enum LevelTypes {
        Open,
        Linear
    };

    public LevelTypes levelType;

    private GameManager _gameManager;
    private Transform _myTransform;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _gameManager.currentLevelType = levelType.ToString();
        _myTransform = transform;
    }

    void Update() {
        if (_gameManager.currentLevelType == GameManager.LEVEL_TYPE_LINEAR)
            _myTransform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}
