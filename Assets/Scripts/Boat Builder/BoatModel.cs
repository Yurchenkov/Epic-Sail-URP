using UnityEngine;
using UnityEngine.UI;

public class BoatModel : MonoBehaviour{

    public static BoatModel instance;

    public BoatsStern testStern;
    public BoatsMast testMast;
    public BoatsSail testSail;

    public delegate void TouchCard();
    public event TouchCard TouchAnother;

    [SerializeField] private BoatsStern _currentStern;
    [SerializeField] private BoatsMast _currentMast;
    [SerializeField] private BoatsSail _currentSail;
    //TODO: убрать отсюда:
    [SerializeField] private Button _nextLevelButton;
    private bool _hasStern = false;
    private bool _hasSail = false;

    private Transform _transform;


    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        _transform = transform;
        //_currentStern = GameManager.Instance.playerData.stern;
        //_currentMast = GameManager.Instance.playerData.mast;
        //_currentSail = GameManager.Instance.playerData.sail;
        //SetStern(testStern);
        //SetMast(testMast);
        //SetSail(testSail);
    }

    void Update()
    {
        RotateModel();
        EnableNextLevelButton();
    }

    public void RotateModel() {
        if (Input.touchCount > 0) {
            float xRot = Input.GetTouch(0).deltaPosition.x;
            //float yRot = Input.GetTouch(0).position.y;
            _transform.RotateAround(Vector3.zero, Vector3.up, - xRot * .5f);
            //_transform.RotateAround(Vector3.zero, Vector3.left, yRot);
        }
    }

    public void SetStern(BoatsStern newStern) {
        if (_currentStern) {
            Destroy(_currentStern.gameObject);
        }

        _currentStern = Instantiate(newStern, Vector3.zero, Quaternion.identity, _transform);
        
        if(_currentMast)
            SetMast(_currentMast);
        _hasStern = true;
    }

    public void SetMast(BoatsMast newMast) {
        if (_currentMast) {
            Destroy(_currentMast.gameObject);
        }
        Vector3 setPosition = Vector3.zero;
        if (_currentStern)
            setPosition = _currentStern.GetMastMountingPoint.localPosition;

        _currentMast = Instantiate(newMast, setPosition, Quaternion.identity, _transform);
        
        if (_currentSail)
            SetSail(_currentSail);
        _hasSail = true;
    }

    public void SetSail(BoatsSail newSail) {
        if (_currentSail) {
            Destroy(_currentSail.gameObject);
        }
        _currentSail = Instantiate(newSail, _currentMast.GetSailMountingPoint.localPosition, Quaternion.identity, _transform);
    }

    private void EnableNextLevelButton() {
        if (_hasSail && _hasStern) {
            _nextLevelButton.gameObject.SetActive(true);
            GameManager.Instance.playerData.CompleteTutorial(Constants.TUTORIAL_WORKSHOP);
        }
    }

    public void StartTouchEvent() {
        TouchAnother?.Invoke();
    }
}
