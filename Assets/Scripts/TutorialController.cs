using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialCanvas;
    public Text tutorialText;

    private Dictionary<string, string> _tutorialsText;
    private GameManager _gameManager;
    private string _tutorialType;

    private void Awake() {
        TutorialTextRepository tutorialTextRepository = new TutorialTextRepository();

        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _tutorialsText = tutorialTextRepository.TutorialsText;
        _tutorialType = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameManager.TAG_PLAYER) && !GameManager.IsTutorialComplete(_tutorialType)) {
            _gameManager.Pause();
            FillTutorialCanvas();
            GameManager.CompleteTutorial(_tutorialType);
        }
    }

    public void ResumeGame() {
        _gameManager.Resume();
    }

    private void FillTutorialCanvas() {
        tutorialCanvas.SetActive(true);
        tutorialText.text = _tutorialsText[_tutorialType];
    }
}
