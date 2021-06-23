using UnityEngine;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialCanvas;
    public Animator handCursorAnimator;

    private GameManager _gameManager;
    private string _tutorialType;

    private void Awake() {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _tutorialType = gameObject.tag;
    }

    public void CloseTutorial() {
        tutorialCanvas.SetActive(false);
        GameManager.CompleteTutorial(_tutorialType);
        ResumeGame();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameManager.TAG_PLAYER) && !GameManager.IsTutorialComplete(_tutorialType))
            ShowTutorial();
    }

    public void ShowTutorial() {
        _gameManager.Pause();
        StartTutorialAnimation();
    }

    private void ResumeGame() {
        _gameManager.Resume();
    }

    private void StartTutorialAnimation() {
        tutorialCanvas.SetActive(true);
        handCursorAnimator.SetTrigger(_tutorialType);
    }
}
