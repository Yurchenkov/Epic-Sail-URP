using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public GameObject popupCanvas;
    public Text popupText;
    public bool isDisposable = true;

    private Dictionary<string, string> _popupText;
    private GameManager _gameManager;
    private string _popupType;

    private void Awake() {
        PopupTextRepository popupTextRepository = new PopupTextRepository();

        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _popupText = popupTextRepository.PopupsText;
        _popupType = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameManager.TAG_PLAYER) && !GameManager.IsPopupViewed(_popupType))
            ShowPopup();
    }

    public void ShowPopup() {
        _gameManager.Pause();
        FillPopupCanvas();

        if (isDisposable)
            GameManager.SetPopupAsViewed(_popupType);
    }

    public void ResumeGame() {
        _gameManager.Resume();
    }

    private void FillPopupCanvas() {
        popupCanvas.SetActive(true);
        popupText.text = _popupText[_popupType];
    }
}
