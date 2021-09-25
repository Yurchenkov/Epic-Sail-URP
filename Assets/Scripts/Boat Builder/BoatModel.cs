using UnityEngine;

public class BoatModel : MonoBehaviour{

    public static BoatModel instance;

    public BoatsStern testStern;
    public BoatsMast testMast;
    public BoatsSail testSail;

    [SerializeField] private BoatsStern _currentStern;
    [SerializeField] private BoatsMast _currentMast;
    [SerializeField] private BoatsSail _currentSail;

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
        SetStern(testStern);
        SetMast(testMast);
        SetSail(testSail);
    }

    void Update()
    {
        //RotateModel();
    }

    public void RotateModel() {
        float xRot = Input.GetTouch(0).position.x;
        float yRot = Input.GetTouch(0).position.y;
        _transform.RotateAround(Vector3.zero, Vector3.up, xRot);
        _transform.RotateAround(Vector3.zero, Vector3.left, yRot);
    }

    public void SetStern(BoatsStern newStern) {
        if (_currentStern) {
            Destroy(_currentStern);
        }
        _currentStern = Instantiate(newStern, Vector3.zero, Quaternion.identity, _transform);
        
        if(_currentMast)
            SetMast(_currentMast);
    }

    public void SetMast(BoatsMast newMast) {
        if (_currentMast) {
            Destroy(_currentMast);
        }
        _currentMast = Instantiate(newMast, _currentStern.GetMastMountingPoint.localPosition, Quaternion.identity, _transform);
        
        if (_currentSail)
            SetSail(_currentSail);
    }

    public void SetSail(BoatsSail newSail) {
        if (_currentSail) {
            Destroy(_currentSail);
        }
        _currentSail = Instantiate(newSail, _currentMast.GetSailMountingPoint.localPosition, Quaternion.identity, _transform);
    }
}
