using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private Button _pauseButton;

    private void Start() {
        GameManager.instance._timer.CountdownIsOver += ResumAfterTimer;
    }

    private void OnDestroy() {
        GameManager.instance._timer.CountdownIsOver -= ResumAfterTimer;
    }

    public void Pause() {
        GameManager.instance.Pause();
        _pauseButton.enabled = false;
    }

    public void Resume() {
        GameManager.instance.Resume();
    }

    public void Restart() {
        GameManager.instance.Restart();
    }

    public void QuitToMainMenu() {
        GameManager.instance.QuitToMainMenu();
    }

    private void ResumAfterTimer() {
        _pauseButton.enabled = true;
    }
}
