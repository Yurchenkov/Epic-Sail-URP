using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject infiniteLevelButton;

    private void Update() {
        if (GameManager.completedTutorials.Contains(Constants.TUTORIAL_TYPE_MOVEMENT))
            infiniteLevelButton.SetActive(true);
    }

    public void StartTutorial() {
        SceneManager.LoadScene(Constants.BUILD_INDEX_TUTORIAL_LINEAR_LEVEL);
    }

    public void StartInfiniteLevel() {
        SceneManager.LoadScene(Constants.BUILD_INDEX_INFINITE_LEVEL);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
