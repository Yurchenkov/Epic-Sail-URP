using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public static float totalCoinCounter = 0f;
    public static List<string> completedTutorials = new List<string>();
    public static List<string> viewedPopups = new List<string>();

    public bool isGamePaused = false;
    public float localCoinCounter = 0f;
    public string currentLevelType;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }

    public void Pause() {
        Time.timeScale = 0f;
        SetPauseGameState(true);
    }

    public void Resume() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
    }

    public void Restart() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu() {
        Time.timeScale = 1f;
        SetPauseGameState(false);
        SceneManager.LoadScene(Constants.BUILD_INDEX_MAIN_MENU);
    }

    private void SetPauseGameState(bool isPaused) {
        isGamePaused = isPaused;
    }

    public void IncreaseLocalCoinCounter() {
        localCoinCounter++;
        IncreaseTotalCoinCounter();
    }

    public static void IncreaseTotalCoinCounter() {
        totalCoinCounter++;
    }

    public static bool IsTutorialComplete(string tutorialType) {
        return completedTutorials.Contains(tutorialType);
    }

    public static bool IsPopupViewed(string popupType) {
        return viewedPopups.Contains(popupType);
    }

    public static void CompleteTutorial(string tutorialType) {
        completedTutorials.Add(tutorialType);
    }

    public static void SetPopupAsViewed(string popupType) {
        viewedPopups.Add(popupType);
    }
}
