using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private Button _pauseButton;

    private void Start() {
        GameManager.Instance._timer.CountdownIsOver += ResumAfterTimer;
    }

    private void OnDestroy() {
        GameManager.Instance._timer.CountdownIsOver -= ResumAfterTimer;
    }

    public void Pause() {
        GameManager.Instance.Pause();
        _pauseButton.enabled = false;
    }

    public void Resume() {
        GameManager.Instance.Resume();
    }

    public void Restart() {
        GameManager.Instance.Restart();
    }

    public void QuitToMainMenu() {
        GameManager.Instance.QuitToMainMenu();
    }

    private void ResumAfterTimer() {
        _pauseButton.enabled = true;
    }
}
