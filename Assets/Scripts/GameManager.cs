using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool isGamePaused = false;
    public string currentLevelType;
    public ResumeTimer _timer;
    public Player playerData;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        playerData = playerData.Load();
        playerData.SetRecordTable();
    }

    private void OnEnable() {
        _timer.CountdownIsOver += ResumeAfterTimer;
    }

    private void OnDisable() {
        _timer.CountdownIsOver -= ResumeAfterTimer;
    }

    public void Pause() {
        Time.timeScale = 0f;
        SetPauseGameState(true);
    }

    public void Resume() {
        _timer.StartTimer();
    }

    public void Restart() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
        playerData.ClosePlayerData();
        SaveLoadManager.Save(playerData);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
        playerData.ClosePlayerData();
        SaveLoadManager.Save(playerData);
        SceneManager.LoadScene(Constants.BUILD_INDEX_MAIN_MENU);
    }

    private void SetPauseGameState(bool isPaused) {
        isGamePaused = isPaused;
    }

    public void ResumeAfterTimer() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
    }
}
