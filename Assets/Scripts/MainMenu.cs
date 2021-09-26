using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        if (GameManager.Instance.playerData.IsTutorialComplete(Constants.TUTORIAL_LEVEL) && GameManager.Instance.playerData.IsTutorialComplete(Constants.TUTORIAL_WORKSHOP)) {
            StartInfiniteLevel();
            return;
        }
        if (GameManager.Instance.playerData.IsTutorialComplete(Constants.TUTORIAL_WORKSHOP)) {
            StartTutorial();
            return;
        }
        StartWorkshopLevel();
    }

    private void StartTutorial() {
        SceneManager.LoadScene(Constants.BUILD_INDEX_TUTORIAL_LINEAR_LEVEL);
    }

    private void StartInfiniteLevel() {
        SceneManager.LoadScene(Constants.BUILD_INDEX_INFINITE_LEVEL);
    }

    private void StartWorkshopLevel() {
        SceneManager.LoadScene(Constants.BUILD_INDEX_WORKSHOP_LEVEL);
    }
}
