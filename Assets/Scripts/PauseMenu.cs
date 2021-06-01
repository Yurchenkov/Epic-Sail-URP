using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private GameManager _gameManager;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
    }

    public void Pause() {
        _gameManager.Pause();
    }

    public void Resume() {
        _gameManager.Resume();
    }

    public void Restart() {
        _gameManager.Restart();
    }

    public void QuitToMainMenu() {
        _gameManager.QuitToMainMenu();
    }
}
