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

    private void Start() {
        _paymentButtonText = paymentButton.GetComponentInChildren<Text>();
        _continuationFee = _initialFee;
        _paymentButtonText.text = _continuationFee.ToString();
    }

    public void PayToContinue() {
        _obstacle.SetActive(false);
        GameManager.totalCoinCounter -= _continuationFee;
        if (_continuationFee < _maximumFee) {
            _continuationFee += _feeIncrement;
        }
        _paymentButtonText.text = _continuationFee.ToString();
        GameManager.instance.Resume();
    }

    public bool CheckPayability() {
        return GameManager.totalCoinCounter > _continuationFee;
    }

    public void OpenLoseWindow(GameObject obstacle) {
        _obstacle = obstacle;
        GameManager.instance.Pause();
        loseWindowCanvas.SetActive(true);
        if (CheckPayability()) {
            paymentButton.enabled = true;
        } else {
            paymentButton.enabled = false;
            paymentButton.gameObject.GetComponent<Graphic>().color = Color.gray;
        }
    }
}
