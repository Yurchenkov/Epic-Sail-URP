using UnityEngine;
using UnityEngine.UI;

public abstract class BoatsPart : MonoBehaviour{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isPaid;
    [SerializeField] private int _price;
    [SerializeField] private bool _isClosed;

    public Sprite GetSprite => _sprite;

    public bool GetPayable => _isPaid;

    public int GetPrice => _price;

    public bool GetClosedStatus => _isClosed;

    public void ToPay() {
        if(!_isClosed)
            _isPaid = true;
    }

}
