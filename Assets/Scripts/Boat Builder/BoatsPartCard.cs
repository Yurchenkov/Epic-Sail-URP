using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BoatsPartCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image image;
    public int partCount;
    public bool isPaid;
    public int price;
    
    public int currentPartsList;
    public bool isClose;

    [SerializeField] private Image _fogImage;
    [SerializeField] private Text priceText;

    private Transform _transform;
    private float scaleMultiplier;
    
    private void Awake() {
        _transform = transform;
        image = GetComponent<Image>();
    }

    private void Start() {
        if (isPaid || isClose) {
            _fogImage.enabled = false;
            priceText.enabled = false;
        } else {
            priceText.text = price.ToString();
        }
    }

    private void SetImageScale(float scale = 1) {
        //FindScaleMultiplier();
        scaleMultiplier = scale;
        _transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
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
    }

    public void OnPointerExit(PointerEventData eventData) {
        SetImageScale();
        SetColor(194f / 255f);
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
                PartsDepository.instance.sterns[partCount].ToPay();
                break;
            case 2:
                PartsDepository.instance.masts[partCount].ToPay();
                break;
            case 3:
                PartsDepository.instance.sails[partCount].ToPay();
                break;
        }
        _fogImage.enabled = false;
        priceText.enabled = false;
    }
}
