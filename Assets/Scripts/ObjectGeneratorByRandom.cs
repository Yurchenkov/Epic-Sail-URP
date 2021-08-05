using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorByRandom : MonoBehaviour {

    public static ObjectGeneratorByRandom instance;

    public GameObject coinPrefab;
    public GameObject[] obstaclePrefabs;
    

    [SerializeField] private int _coinCount;
    [SerializeField] private int _obstacleCount;
    [SerializeField] private int _gridWidth = 0;

    private int _gridLength = 0;
    private float _offsetZ;
    private float _offsetX;
    private Transform _transform;
    private List<Cell> _pos;
    private bool isFirstPiece = true;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        _transform = transform;
    }

    public void CreateObject(Vector3 createPoint, GameObject objectPrefab) {
        Instantiate(objectPrefab, createPoint, Quaternion.identity, _transform);
    }

    public void FillWaterArea(Transform waterArea, int coinCount, int obstacleCount) {
        if (!isFirstPiece) {
            _coinCount = coinCount;
            _obstacleCount = obstacleCount;
            CreateListPosition(waterArea);
            FillGridWithRandomObjects(_obstacleCount);
            FillGridWithObjects(coinPrefab, _coinCount);
        } else {
            isFirstPiece = false;
        }
    }

    private void FillGridWithObjects(GameObject objectPrefab, int count) {
        if (count > 0) {
            int rowDepth = Mathf.CeilToInt(FindMaxSide(objectPrefab) / _offsetZ);
            int columnDepth = Mathf.CeilToInt(FindMaxSide(objectPrefab) / _offsetX);
            int placedObject = 0;

            while (placedObject < count && _pos.Count > 0) {
                placedObject++;
                int index = Random.Range(0, _pos.Count);
                if (_pos[index] != null) {
                    Vector3 objectPosition = _pos[index].Position;
                    CreateObject(objectPosition, objectPrefab);
                    Cell tempCell = _pos[index];
                    _pos.RemoveAt(index);
                    DeleteElementsAtDepth(tempCell, rowDepth, columnDepth);
                } else {
                    _pos.RemoveAt(index);
                    placedObject--;
                }
            }
        }
    }

    private void FillGridWithRandomObjects(int count) {
        for (int i = 0; i < count; i++) {
            GameObject tempObject = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            FillGridWithObjects(tempObject, 1);
        }
    }

    private void DeleteElementsAtDepth(Cell cell, int rowDepth, int columnDepth) {
        if (rowDepth > 1) {
            if (cell.Right != null)
                DeleteElementsAtDepth(cell.Right, rowDepth - 1, columnDepth);
            if (cell.Left != null)
                DeleteElementsAtDepth(cell.Left, rowDepth - 1, columnDepth);
        }
        if (columnDepth > 1) {
            if (cell.Top != null)
                DeleteElementsAtDepth(cell.Top, rowDepth, columnDepth - 1);
            if (cell.Down != null)
                DeleteElementsAtDepth(cell.Down, rowDepth, columnDepth - 1);
        }
        _pos.Remove(cell);
    }

    private void SetGridParemeters(Bounds area, float objectSize) {
        int x = (int)(area.size.x / objectSize);
        int z = (int)((area.size.z - .3f * area.size.z) / objectSize);
        _gridLength = x;
        if (_gridWidth <= 0 || _gridWidth >= z) {
            _gridWidth = z;
            _offsetZ = objectSize;
        } else {
            _offsetZ = (area.size.z - .3f * area.size.z) / _gridWidth;
        }
        _offsetX = objectSize;
    }

    private void CreateListPosition(Transform waterArea) {
        Bounds boundsArea = waterArea.GetComponent<Renderer>().bounds;
        float maxObjectSize = FindMaxSizeSmallestObject();
        Vector3 startGridPosition = new Vector3(waterArea.position.x - boundsArea.extents.x, .7f, boundsArea.center.z);

        SetGridParemeters(boundsArea, maxObjectSize);
        _pos = new List<Cell>();

        for (int i = 0; i < _gridLength; i++) {
            for (int j = 0; j < _gridWidth; j++) {
                float positionX = startGridPosition.x + i * maxObjectSize + maxObjectSize / 2;
                float positionZ = startGridPosition.z + (_gridWidth - 1) * _offsetZ / 2 - j * _offsetZ;
                Vector3 position = new Vector3(positionX, .7f, positionZ);
                Cell cell = new Cell(position);
                _pos.Add(cell);
                if (i - 1 > 0) {
                    int index = _pos.Count - 1;
                    _pos[index].Down = _pos[index - _gridWidth];
                    _pos[index - _gridWidth].Top = _pos[index];
                }
                if (j - 1 > 0) {
                    int index = _pos.Count - 1;
                    _pos[index].Left = _pos[index - 1];
                    _pos[index - 1].Right = _pos[index];
                }
            }
        }
    }

    private float FindMaxSizeSmallestObject() {
        float maxSide = FindMaxSide(obstaclePrefabs[0]);
        foreach(GameObject objectPrefab in obstaclePrefabs) {
            float tempSide = FindMaxSide(objectPrefab);
            maxSide = maxSide > tempSide ? tempSide : maxSide; 
        }

        return Mathf.Min(FindMaxSide(coinPrefab), maxSide);
    }

    private float FindMaxSide(GameObject objectPrefab) {
        Bounds objectBounds = objectPrefab.GetComponent<Renderer>().bounds;

        return Mathf.Max(objectBounds.size.x, objectBounds.size.z);
    }

    public class Cell {

        public Vector3 Position { get; set; }
        public Cell Top { get; set; }
        public Cell Down { get; set; }
        public Cell Right { get; set; }
        public Cell Left { get; set; }

        public Cell(Vector3 position) {
            Position = position;
            Top = null;
            Down = null;
            Right = null;
            Left = null;
        }
    }
}
