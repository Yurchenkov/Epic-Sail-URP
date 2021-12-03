using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floater : MonoBehaviour{

    public float underWaterDrag = 5f;
    public float underWaterAngularDrag = 1.5f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    //public float floatingPower = 15f;

    [SerializeField] private Transform[] floaters;
    [SerializeField] private float _upSpeed = 1f;
    [SerializeField] private float _upSpeedLimit = 1.25f;
    [SerializeField] private Transform _waterLine;

    private Vector3[] waterLinePoints;
    protected Vector3 smoothVectorRotation;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _defaultHeight = 0f;
    private bool _isUnderWater = false;
    private WaterPhisics _waterPhisics;
    [SerializeField]private float _waterHeight;
    //private int _floatersUnderWater = 0;
    private Vector3 _flowDirection = Vector3.zero;
    //private Vector3 _waterNormal;


    private void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        waterLinePoints = new Vector3[floaters.Length];
        _waterLine = floaters[0];
    }

    private void FixedUpdate() {
        RotateBoat();
        CheckWaterDrag();

        _waterHeight = _waterPhisics ? _waterPhisics.GetWaterHeightAtPosition(_transform.position) : _defaultHeight;
        float heightDifference = (_waterHeight - _waterLine.position.y) * _upSpeed;
        _rigidbody.AddForce(new Vector3(0f, Mathf.Clamp(Mathf.Abs(Physics.gravity.y), 0, Mathf.Abs(Physics.gravity.y) * _upSpeedLimit) * heightDifference, 0f) - _flowDirection, ForceMode.Acceleration);
        

        ////    float displacementMultiplier = Mathf.Clamp01((waterHeight - transform.position.y) / underWaterAngularDrag) * underWaterDrag;
        ////    _rigidbody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
    }

    private void CheckWaterDrag() {
        _isUnderWater = false;
            float difference = _waterLine.position.y - _waterHeight;
            if (difference < 0)
                _isUnderWater = true;
        SwitchState(_isUnderWater);
    }

    private void RotateBoat() {
        if (_waterPhisics) {
            for (int i = 0; i < floaters.Length; i++) {
                waterLinePoints[i] = floaters[i].position;
                waterLinePoints[i].y = _waterPhisics.GetWaterHeightAtPosition(waterLinePoints[i]);
            }
        } else return;

        Vector3 targetUp = GetNormal(waterLinePoints);
        targetUp = Vector3.SmoothDamp(transform.up, targetUp, ref smoothVectorRotation, 0.5f);
        _rigidbody.rotation = Quaternion.FromToRotation(_transform.up, targetUp) * _rigidbody.rotation;
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
            _flowDirection = _waterPhisics.flowDirection;
        }
    }

    public Vector3 GetNormal(Vector3[] points) {
        //https://www.ilikebigbits.com/2015_03_04_plane_from_points.html
        if (points.Length < 3)
            return Vector3.up;

        var center = GetCenter(points);

        float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

        for (int i = 0; i < points.Length; i++) {
            var r = points[i] - center;
            xx += r.x * r.x;
            xy += r.x * r.y;
            xz += r.x * r.z;
            yy += r.y * r.y;
            yz += r.y * r.z;
            zz += r.z * r.z;
        }

        var det_x = yy * zz - yz * yz;
        var det_y = xx * zz - xz * xz;
        var det_z = xx * yy - xy * xy;

        if (det_x > det_y && det_x > det_z)
            return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
        if (det_y > det_z)
            return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
        else
            return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;

    }

    public static Vector3 GetCenter(Vector3[] points) {
        var center = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
            center += points[i] / points.Length;
        return center;
    }
}
