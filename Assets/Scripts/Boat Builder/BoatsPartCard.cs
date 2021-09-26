using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BoatsPartCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image image;
    public int partCount;
    public bool isPaid = false;
    public int price;
    
    public int currentPartsList;
    public bool isClose;

    [SerializeField] private Image _fogImage;
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _paymentPanel;

    private Transform _transform;
    private float _scaleMultiplier;
    
    private void Awake() {
        _transform = transform;
        image = GetComponent<Image>();
    }

    private void Start() {
        if (isPaid || isClose) {
            _fogImage.enabled = false;
            _priceText.enabled = false;
        } else {
            _priceText.text = price.ToString();
        }
    }

    private void SetImageScale(float scale = 1) {
        //FindScaleMultiplier();
        _scaleMultiplier = scale;
        _transform.localScale = new Vector3(_scaleMultiplier, _scaleMultiplier, 1);
    }

    //private void FindScaleMultiplier() {
    //    float auxillaryCoef = Mathf.Abs(_transform.localPosition.x) / 350f;
    //    scaleMultiplier = 1.2f - 0.2f * Mathf.Clamp(auxillaryCoef, 0, 1);
    //}

    //public void OnPointerDown(PointerEventData eventData) {
    //    SetImageScale();
    //}

    public void OnPointerEnter(PointerEventData eventData) {
        SetImageScale(1.2f);
        SetColor(1);
        if (!isClose && !isPaid)
            _paymentPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        SetImageScale();
        SetColor(194f / 255f);
        _paymentPanel.SetActive(false);
    }

    private void SetColor(float color) {
        Color tempColor = image.color;
        tempColor.a = color;
        image.color = tempColor;
    }

    public void ToPay() {
        GameManager.Instance.playerData.ReduceTotalMoney(price);
        switch (currentPartsList) {
            case 1:
                PartsRepository.instance.sterns[partCount].ToPay();
                BoatModel.instance.SetStern(BoatModel.instance.testStern);
                break;
            case 2:
                PartsRepository.instance.masts[partCount].ToPay();
                break;
            case 3:
                PartsRepository.instance.sails[partCount].ToPay();
                BoatModel.instance.SetMast(BoatModel.instance.testMast);
                BoatModel.instance.SetSail(BoatModel.instance.testSail);
                break;
        }
        _fogImage.enabled = false;
        _priceText.enabled = false;
        isPaid = true;
    }

}
