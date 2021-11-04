using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floater : MonoBehaviour{

    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1.5f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;

    [SerializeField] private Transform[] floaters;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _defaultHeight = 0f;
    private bool _isUnderWater = false;
    private WaterPhisics _waterPhisics;
    private float _waterHeight;
    private int _floatersUnderWater = 0;


    private void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        _waterHeight = _waterPhisics ? _waterPhisics.GetWaterHeightAtPosition(_transform.position) : _defaultHeight;
    }

    private void FixedUpdate() {
        _floatersUnderWater = 0;
        for (int index = 0; index < floaters.Length; index++) {
            float difference = floaters[index].position.y - _waterHeight;
            if (difference < 0) {
                _rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[index].position, ForceMode.Force);
                _floatersUnderWater++;
                if (!_isUnderWater) {
                    _isUnderWater = true;
                    SwitchState(true);
                }
            }
        }
        if (_isUnderWater && _floatersUnderWater == 0) {
            _isUnderWater = false;
            SwitchState(false);
        }
        //if (transform.position.y < waterHeight) {
        //    float displacementMultiplier = Mathf.Clamp01((waterHeight - transform.position.y) / underWaterAngularDrag) * underWaterDrag;
        //    _rigidbody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
        //}
    }

    private void SwitchState(bool isUnderwater) {
        if (isUnderwater) {
            _rigidbody.drag = underWaterDrag;
            _rigidbody.angularDrag = underWaterAngularDrag;
        } else {
            _rigidbody.drag = airDrag;
            _rigidbody.angularDrag = airAngularDrag;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 4) {
            _waterPhisics = other.gameObject.GetComponent<WaterPhisics>();
        }
    }
}
