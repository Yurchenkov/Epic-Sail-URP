using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour {

    public GameObject loseWindowCanvas;
    public Button paymentButton;

    [SerializeField] private float _initialFee;
    [SerializeField] private float _feeIncrement;
    [SerializeField] private float _maximumFee;
    [SerializeField] private Text _allMoney;


    private float _continuationFee;
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

        GameManager.playerData.DeductMoney((int)_continuationFee); //.totalCoinCounter -= _continuationFee;
        if (_continuationFee < _maximumFee) _continuationFee += _feeIncrement;
        else _continuationFee = _maximumFee;
        _paymentButtonText.text = _continuationFee.ToString();
        GameManager.instance.Resume();
    }

    public bool IsPayable() {
        return GameManager.playerData.allMoney > _continuationFee;
    }

    public void OpenLoseWindow(GameObject obstacle) {
        _obstacle = obstacle;
        GameManager.instance.Pause();
        _allMoney.text = "Всего денег: " + GameManager.playerData.allMoney;
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
