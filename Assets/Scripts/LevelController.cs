using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public enum LevelTypes {
        Open,
        Linear
    };

    public LevelTypes levelType;

    private GameManager _gameManager;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _gameManager.currentLevelType = levelType.ToString();
    }

    void Update() {
        if (_gameManager.currentLevelType == GameManager.LEVEL_TYPE_LINEAR)
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}
