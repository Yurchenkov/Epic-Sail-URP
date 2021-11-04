using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhisics : MonoBehaviour {

    [SerializeField] private float _waveHeight = 4f;
    [SerializeField] private float _mainTiling = .001f;
    [SerializeField] private float _mainSpeed = .3f;
    [SerializeField] private float _mainDeductible = 0f;
    [SerializeField] private float _secondaryTiling = 0.003f;
    [SerializeField] private float _secondarySpeed = .5f;
    [SerializeField] private float _secondaryDeductible = 0f;

    private Material _waterMaterial;
    private Transform _transform;
    private Texture2D _wavesDisplacement;


    private void Start() {
        _transform = transform;
        SetMaterial();
    }

    public float GetWaterHeightAtPosition(Vector3 position) {
        UpdateParameters();
        return _transform.position.y + Mathf.Clamp(GetWavesHeight(position, _mainTiling, _mainDeductible, _mainSpeed) - GetWavesHeight(position, _secondaryTiling, _secondaryDeductible, _secondarySpeed),0f,1f) * _waveHeight * transform.localScale.x;
    }

    private float GetWavesHeight(Vector3 position, float tiling, float deductible, float speed) {
        float speedMultiplier = (Time.time / 20) * speed;
        return Mathf.Clamp(_wavesDisplacement.GetPixelBilinear(position.x * tiling + speedMultiplier, position.z * tiling).r - Mathf.Clamp(deductible, 0f, 1f), 0f, 1f);
    }
    private void SetMaterial() {
        _waterMaterial = GetComponent<Renderer>().sharedMaterial;
        _wavesDisplacement = (Texture2D)_waterMaterial.GetTexture("_waveTexture");
        UpdateParameters();
    }

    private void UpdateParameters() {
        _waveHeight = _waterMaterial.GetFloat("_waveHeight");
        _mainTiling = _waterMaterial.GetFloat("_mainTiling");
        _mainSpeed = _waterMaterial.GetFloat("_mainSpeed");
        _mainDeductible = _waterMaterial.GetFloat("_mainDeductible");
        _secondaryTiling = _waterMaterial.GetFloat("_secondaryTiling");
        _secondarySpeed = _waterMaterial.GetFloat("_secondarySpeed");
        _secondaryDeductible = _waterMaterial.GetFloat("_secondaryDeductible");
    }
    //private void OnValidate() {
    //    if (!_waterMaterial)
    //        SetMaterial();
    //    //SetParameters();
    //    UpdateMaterial();
    //}

    //private void UpdateMaterial() {
    //    _waterMaterial.SetFloat("_waveHeight", _waveHeight);

    //    _waterMaterial.SetFloat("_mainTiling", _mainTiling);
    //    _waterMaterial.SetFloat("_mainSpeed", _mainSpeed);
    //    _waterMaterial.SetFloat("_mainDeductible", _mainDeductible);

    //    _waterMaterial.SetFloat("_secondaryTiling", _secondaryTiling);
    //    _waterMaterial.SetFloat("_secondarySpeed", _secondarySpeed);
    //    _waterMaterial.SetFloat("_secondaryDeductible", _secondaryDeductible);
    //    UpdateParameters();
    //}


    //public float GetVertexHeight(Vector3 position) {
    //    for (int index = 0; index < _waterMesh.vertices.Length; index++) {
    //        if (closestVertexIndex == -1)
    //            closestVertexIndex = index;

    //        float distance = Vector3.Distance(_waterMesh.vertices[index] + _transform.position, position);
    //        float closestDistance = Vector3.Distance(_waterMesh.vertices[closestVertexIndex] + _transform.position, position);

    //        if (distance < closestDistance)
    //            closestVertexIndex = index;
    //    }
    //    Debug.Log(_waterMesh.vertices[closestVertexIndex].y);
    //    return _waterMesh.vertices[closestVertexIndex].y + _transform.position.y;
    //}
}
