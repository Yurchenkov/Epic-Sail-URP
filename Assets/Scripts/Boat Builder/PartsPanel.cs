using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartsPanel : MonoBehaviour {

    [SerializeField] private BoatsPartCard cardPrefab;
    [SerializeField] private RectTransform _contentPanel;

    private int _currentPartsListNumber;
    private float _cardsOffsetX = 350;

    private void Start() {
        //FillPanelByListNumber(1);
    }

    //TODO: Переписать логику передачи списков частей:

    public void FillPanelByListNumber(int PartsDepositoryListNumber) {
        ClearContent();
        _currentPartsListNumber = PartsDepositoryListNumber;
        switch (PartsDepositoryListNumber) {
            case 1:
                FillPartsPanel(PartsRepository.instance.sterns);
                break;
            case 2:
                FillPartsPanel(PartsRepository.instance.masts);
                break;
            case 3:
                FillPartsPanel(PartsRepository.instance.sails);
                break;
        }
    }

    private List<BoatsPart> ConvertToBoatsPartList(IEnumerable<BoatsPart> partList) {
        List<BoatsPart> resultList = new List<BoatsPart>();
        foreach (Object element in partList) {
            BoatsPart resultElem = element as BoatsPart;
            resultList.Add(resultElem);
        }
        return resultList;
    }

    public void FillPartsPanel(IEnumerable<BoatsPart> partsList) {
        //_currentPartList = partsList;
        List<BoatsPart> boatsParts = ConvertToBoatsPartList(partsList);
        foreach (BoatsPart part in boatsParts) {
            int i = 0;
            CreateCard(i, i, part);
            i++;
        }

        float height = _contentPanel.sizeDelta.y;
        _contentPanel.sizeDelta = new Vector2(boatsParts.Count * 350, height);
    }

    private BoatsPartCard CreateCard(int partNumber, int cardNumber, BoatsPart part) {
        BoatsPartCard card = Instantiate(cardPrefab, _contentPanel);

        card.GetComponent<RectTransform>().localPosition = new Vector3(-2 * _cardsOffsetX + cardNumber * _cardsOffsetX, 0, 0);
        card.image.sprite = part.GetSprite;
        card.price = part.GetPrice;
        card.isPaid = part.GetPayable;
        card.partCount = partNumber;
        card.currentPartsList = _currentPartsListNumber;
        card.isClose = part.GetClosedStatus;

        return card;
    }

    private void ClearContent() {
        Transform content = _contentPanel.transform;
        for (int i = 0; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
