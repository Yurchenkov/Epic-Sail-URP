using UnityEngine;

public class LinearObjectsGenerator : MonoBehaviour {

    public static LinearObjectsGenerator instance;

    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    [SerializeField] private int _gridWidth = 0;
    [SerializeField] private int _maxCoinPerTrail = 8;

    private float _offsetZ;
    private Transform _transform;
    private float _coinSize;
    private float _obstacleSize;
    private Vector3 startLinePosition;
    private float lengthAllTrail = 0;
    private float pieceLength;
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

    public void FillWaterArea(Transform waterArea) {
        if (!isFirstPiece) {
            Bounds boundsArea = waterArea.GetComponent<Renderer>().bounds;
            float maxObjectSize = FindMaxSizeSmallestObject();
            startLinePosition = new Vector3(waterArea.position.x - boundsArea.extents.x, .7f, boundsArea.center.z);
            SetPlacedParemeters(boundsArea, maxObjectSize);
            PlaceObjects();
        } else
            isFirstPiece = false;
    }

    private void PlaceObjects() {
        int lineNumber = 0;
        //TODO: Uncoment for offsets next coint trail per line
        //int lastLine = 0;
        //int offsetPerLine = 0;
        lengthAllTrail = 0;


        while (lengthAllTrail < pieceLength) {
            bool placedObstacleInEnd = Random.Range(0, 2) > 0;
            bool placedObstacleInOtherLine = Random.Range(0, 2) > 0;
            int trailLenght = _maxCoinPerTrail - Random.Range(0, 3);

            lineNumber = Random.Range(0, _gridWidth);
            //TODO: Uncoment for offsets next coint trail per line
            //if (lastLine != lineNumber) {
            //    offsetPerLine = Random.Range(0, 5);
            //}
            //lastLine = lineNumber;
            //startLinePosition.x -= offsetPerLine * _coinSize;

            for (int j = 0; j < trailLenght; j++) {
                Vector3 position = FindObjectPosition(_coinSize, lineNumber);
                CreateObject(position, coinPrefab);
                if (j == trailLenght - 1) {
                    if (placedObstacleInOtherLine) {
                        float minStartPosX = startLinePosition.x - trailLenght * _coinSize + _obstacleSize / 2;
                        float maxStartPosX = startLinePosition.x - _obstacleSize / 2;
                        float leftSide = lineNumber * _offsetZ - _obstacleSize / 2;
                        float rightSide = (_gridWidth - lineNumber) * _offsetZ - _obstacleSize / 2;

                        if (minStartPosX < maxStartPosX && (leftSide > _obstacleSize || rightSide > _obstacleSize)) {
                            float positionX = Random.Range(minStartPosX, maxStartPosX);
                            int leftPossabilityCount = (int)(leftSide / _obstacleSize);
                            int rightPossabilityCount = (int)(rightSide / _obstacleSize);

                            if (leftPossabilityCount > 0 && rightPossabilityCount > 0) {
                                switch (Random.Range(1, 3)) {
                                    case 1:
                                        int choise = Random.Range(0, 2);
                                        if (choise > 0)
                                            PlacedObstacleInRange(positionX, position.z + leftSide - _obstacleSize / 2, position.z + _obstacleSize);
                                        else
                                            PlacedObstacleInRange(positionX, position.z - _obstacleSize, position.z - rightSide + _obstacleSize / 2);
                                        break;
                                    case 2:
                                        PlacedObstacleInRange(positionX, position.z + leftSide - _obstacleSize / 2, position.z + _obstacleSize);
                                        PlacedObstacleInRange(positionX, position.z - _obstacleSize, position.z - rightSide + _obstacleSize / 2);
                                        break;
                                }
                            } else if (leftPossabilityCount > 0) {
                                PlacedObstacleInRange(positionX, position.z + leftSide - _obstacleSize / 2, position.z + _obstacleSize);
                            } else {
                                PlacedObstacleInRange(positionX, position.z - _obstacleSize, position.z - rightSide + _obstacleSize / 2);
                            }
                        }
                    }
                    if (placedObstacleInEnd) {
                        position = FindObjectPosition(_obstacleSize, lineNumber);
                        CreateObject(position, obstaclePrefab);
                    }
                }
            }
        }
    }

    private Vector3 FindObjectPosition(float objectSize, int lineNumber) {
        float positionX = startLinePosition.x + objectSize;
        float positionZ = startLinePosition.z + (_gridWidth - 1) * _offsetZ / 2 - lineNumber * _offsetZ;
        startLinePosition.x = positionX + objectSize / 2;
        lengthAllTrail += objectSize + objectSize / 2;
        return new Vector3(positionX, .7f, positionZ);
    }

    private void PlacedObstacleInRange(float positionX, float maxPositionZ, float minPisotionZ) {
        float positionZ = Random.Range(minPisotionZ, maxPositionZ);
        Vector3 obstaclePosition = new Vector3(positionX, .7f, positionZ);
        CreateObject(obstaclePosition, obstaclePrefab);
    }

    private void SetPlacedParemeters(Bounds area, float objectSize) {
        pieceLength = area.size.x;
        int x = (int)((area.size.x / (objectSize)) / 2);
        int z = (int)((area.size.z - .3f * area.size.z) / objectSize);

        if (_gridWidth <= 0 || _gridWidth >= z) {
            _gridWidth = z;
            _offsetZ = objectSize;
        } else {
            _offsetZ = (area.size.z - .3f * area.size.z) / _gridWidth;
        }
    }

    private float FindMaxSizeSmallestObject() {
        _coinSize = FindMaxSide(coinPrefab);
        _obstacleSize = FindMaxSide(obstaclePrefab);
        return Mathf.Min(_coinSize, _obstacleSize);
    }

    private float FindMaxSide(GameObject objectPrefab) {
        Bounds objectBounds = objectPrefab.GetComponent<Renderer>().bounds;

        return Mathf.Max(objectBounds.size.x, objectBounds.size.z);
    }
}
