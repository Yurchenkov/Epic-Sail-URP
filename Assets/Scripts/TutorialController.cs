using UnityEngine;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialCanvas;
    public Animator handCursorAnimator;

    private string _tutorialType;

    private void Awake() {
        _tutorialType = gameObject.tag;
    }

    public void CloseTutorial() {
        tutorialCanvas.SetActive(false);
        GameManager.CompleteTutorial(_tutorialType);
        ResumeGame();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER) && !GameManager.IsTutorialComplete(_tutorialType))
            ShowTutorial();
    }

    public void ShowTutorial() {
        GameManager.instance.Pause();
        StartTutorialAnimation();
    }

    private void ResumeGame() {
        GameManager.instance.ResumeAfterTimer();
    }

    private void StartTutorialAnimation() {
        tutorialCanvas.SetActive(true);
        handCursorAnimator.SetTrigger(_tutorialType);
    }
}
