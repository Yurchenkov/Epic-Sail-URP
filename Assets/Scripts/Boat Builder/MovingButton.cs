using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButton : MonoBehaviour{

    private RectTransform defaultPosition;
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        defaultPosition = _rectTransform;
    }
    public void MoveButton() {
        _rectTransform.localPosition = new Vector3(-126, defaultPosition.localPosition.y, defaultPosition.localPosition.z);
    }

    public void BackPosition() {
        _rectTransform.localPosition = new Vector3(-60, defaultPosition.localPosition.y, defaultPosition.localPosition.z);
    }
}
