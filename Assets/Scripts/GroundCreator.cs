using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour {

    public static GroundCreator instance;

    [SerializeField] private Transform _boat;
    [SerializeField] private GroundPiece[] _pieces;
    [SerializeField] private int _coinCount; //TODO: Delete after test
    [SerializeField] private int _obstacleCount;

    private List<GroundPiece> _createdPieces = new List<GroundPiece>();
    private Transform _transform;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        _transform = transform;
    }

    private void Start() {
        CreatePiece(2);
    }

    public void DeletePiece(GameObject pieceObject) {
        Destroy(pieceObject);
        _createdPieces.RemoveAt(0);
        CreatePiece();
    }

    public void CreatePiece() {
        GroundPiece newPiece = Instantiate(_pieces[Random.Range(0, _pieces.Length)], _transform);
        Vector3 instantiatePosition;

        if (_createdPieces.Count > 0)
            instantiatePosition = _createdPieces[_createdPieces.Count - 1].endPosition.position - newPiece.startPosition.localPosition * newPiece.scaleX;
        else
            instantiatePosition = Vector3.zero;
        newPiece.transform.position = instantiatePosition;
        //ObjectGeneratorByRandom.instance.FillWaterArea(newPiece.waterArea, _coinCount, _obstacleCount);
        LinearObjectsGenerator.instance.FillWaterArea(newPiece.waterArea);
        _createdPieces.Add(newPiece);
    }

    private void CreatePiece(int count) {
        for (int i = 0; i < count; i++)
            CreatePiece();
    }
}