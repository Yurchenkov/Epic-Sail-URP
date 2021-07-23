using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorByRandom : MonoBehaviour {

    public static ObjectGeneratorByRandom instance;

    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    [SerializeField] private int _coinCount;
    [SerializeField] private int _obstacleCount;
    [SerializeField] private int _gridWidth = 0;
    [SerializeField] private int _maxCoinPerTrail = 8;


    private int _gridLength = 0;
    private float _offsetZ;
    private float _offsetX;
    private Transform _transform;
    private List<Cell> _pos;
    private Vector3 _bungVector = new Vector3(-10, -10, -10);

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
        _coinCount = coinCount;
        _obstacleCount = obstacleCount;
        CreateListPosition(waterArea);
        FillGridWithObjects(obstaclePrefab, _obstacleCount);
        FillGridWithObjects(coinPrefab, _coinCount);
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
                    DeleteElementsAtDepth(tempCell, rowDepth, columnDepth, 0);
                } else {
                    placedObject--;
                    _pos.RemoveAt(index);
                }

            }
        }
    }

    private void DeleteElementsAtDepth(Cell cell, int rowDepth, int columnDepth, int distributionDirection) {
        if (rowDepth > 1 || columnDepth > 1) {
            switch (distributionDirection) {
                case 0:
                    if (cell.Top != null)
                        DeleteElementsAtDepth(cell.Top, rowDepth, columnDepth - 1, 1);
                    if (cell.Right != null)
                        DeleteElementsAtDepth(cell.Right, rowDepth - 1, columnDepth, 2);
                    if (cell.Down != null)
                        DeleteElementsAtDepth(cell.Down, rowDepth, columnDepth - 1, 3);
                    if (cell.Left != null)
                        DeleteElementsAtDepth(cell.Left, rowDepth - 1, columnDepth, 4);
                    cell.Position = _bungVector;
                    DeletePositionsElement(cell);
                    break;

                case 1:
                    if (cell.Top != null)
                        DeleteElementsAtDepth(cell.Top, rowDepth, columnDepth - 1, 1);
                    if (cell.Right != null)
                        DeleteElementsAtDepth(cell.Right, rowDepth - 1, columnDepth, 2);
                    if (cell.Left != null)
                        DeleteElementsAtDepth(cell.Left, rowDepth - 1, columnDepth, 4);
                    cell.Position = _bungVector;
                    DeletePositionsElement(cell);
                    break;

                case 2:
                    if (cell.Left != null)
                        DeleteElementsAtDepth(cell.Left, rowDepth - 1, columnDepth, 2);
                    cell.Position = _bungVector;
                    DeletePositionsElement(cell);
                    break;

                case 3:
                    if (cell.Right != null)
                        DeleteElementsAtDepth(cell.Right, rowDepth - 1, columnDepth, 2);
                    if (cell.Down != null)
                        DeleteElementsAtDepth(cell.Down, rowDepth, columnDepth - 1, 3);
                    if (cell.Left != null)
                        DeleteElementsAtDepth(cell.Left, rowDepth - 1, columnDepth, 4);
                    cell.Position = _bungVector;
                    DeletePositionsElement(cell);
                    break;

                case 4:
                    if (cell.Right != null)
                        DeleteElementsAtDepth(cell.Right, rowDepth - 1, columnDepth, 4);
                    cell.Position = _bungVector;
                    DeletePositionsElement(cell);
                    break;
            }
        } else {
            cell.Position = _bungVector;
            DeletePositionsElement(cell);
        }

    }

    private void DeletePositionsElement(Cell cell) {
        cell = null;
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
                Cell cell = new Cell();
                cell.Position = position;
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
        return Mathf.Min(FindMaxSide(coinPrefab), FindMaxSide(obstaclePrefab));
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

        public Cell() {
            Top = null;
            Down = null;
            Right = null;
            Left = null;
        }
    }
}
