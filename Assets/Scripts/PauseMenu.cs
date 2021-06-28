using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public void Pause() {
        GameManager.instance.Pause();
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
}
