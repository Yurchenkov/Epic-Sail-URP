using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour {

    public GameObject loseWindowCanvas;
    public Button paymentButton;

    [SerializeField] private int _initialFee;
    [SerializeField] private int _feeIncrement;
    [SerializeField] private int _maximumFee;
    [SerializeField] private Text _totalMoneyAmount;
    [SerializeField] private Text _collectedMoneyAmount;

    private int _continuationFee;
    private GameObject _obstacle;
    private Text _paymentButtonText;

    private void Awake() {
        _paymentButtonText = paymentButton.GetComponentInChildren<Text>();
        _continuationFee = _initialFee;
        _paymentButtonText.text = _continuationFee.ToString();
    }

    public void PayToContinue() {
        if (_obstacle != null)
            _obstacle.SetActive(false);

        GameManager.instance.playerData.ReduceTotalMoney(_continuationFee);

        if (_continuationFee < _maximumFee)
            _continuationFee += _feeIncrement;
        else
            _continuationFee = _maximumFee;

        _paymentButtonText.text = _continuationFee.ToString();
        GameManager.instance.Resume();
    }

    public bool IsPayable() {
        return GameManager.instance.playerData.totalMoney > _continuationFee;
    }

    public void OpenLoseWindow(GameObject obstacle) {
        _obstacle = obstacle;
        GameManager.instance.Pause();
        _totalMoneyAmount.text = GameManager.instance.playerData.totalMoney.ToString();
        _collectedMoneyAmount.text = GameManager.instance.playerData.levelMoney.ToString();
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
