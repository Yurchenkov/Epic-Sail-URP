using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public GameObject popupCanvas;
    public Text popupText;
    public bool isDisposable = true;

    private Dictionary<string, string> _popupText;
    private string _popupType;

    private void Awake() {
        PopupTextRepository popupTextRepository = new PopupTextRepository();

        _popupText = popupTextRepository.PopupsText;
        _popupType = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER) && !GameManager.playerData.IsPopupViewed(_popupType)) {
            ShowPopup();
            if (CompareTag(Constants.TAG_TUTORIAL_LEVEL_COMPLETION))
                GameManager.playerData.CompleteTutorial(Constants.TUTORIAL_LEVEL);
        }
    }

    public void ShowPopup() {
        GameManager.instance.Pause();
        FillPopupCanvas();

        if (isDisposable)
            GameManager.playerData.SetPopupAsViewed(_popupType);
    }

    public void ResumeGame() {
        GameManager.instance.ResumeAfterTimer();
    }

    private void FillPopupCanvas() {
        popupCanvas.SetActive(true);
        popupText.text = _popupText[_popupType];
    }
}
