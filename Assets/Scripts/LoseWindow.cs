using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour {

    public GameObject loseWindowCanvas;
    public Button paymentButton;

    [SerializeField] private float _initialFee;
    [SerializeField] private float _feeIncrement;
    [SerializeField] private float _maximumFee;

    private float _continuationFee;
    private GameObject _obstacle;
    private Text _paymentButtonText;

    private void Awake() {
        _paymentButtonText = paymentButton.GetComponentInChildren<Text>();
        _continuationFee = _initialFee;
        _paymentButtonText.text = _continuationFee.ToString();
    }

    public void PayToContinue() {
        _obstacle.SetActive(false);
        GameManager.totalCoinCounter -= _continuationFee;
        if (_continuationFee < _maximumFee) _continuationFee += _feeIncrement;
        else _continuationFee = _maximumFee;
        _paymentButtonText.text = _continuationFee.ToString();
        GameManager.instance.Resume();
    }

    public bool IsPayable() {
        return GameManager.totalCoinCounter > _continuationFee;
    }

    public void OpenLoseWindow(GameObject obstacle) {
        _obstacle = obstacle;
        GameManager.instance.Pause();
        loseWindowCanvas.SetActive(true);
        ShowPaymentButton();
    }

    private void ShowPaymentButton() {
        if (IsPayable()) {
            paymentButton.enabled = true;
        } else {
            paymentButton.enabled = false;
            paymentButton.gameObject.GetComponent<Graphic>().color = Color.gray;
        }
    }
}
