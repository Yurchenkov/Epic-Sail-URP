using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public static CoinGenerator instance;

    public GameObject coinPrefab;

    private Transform _transform;
    private List<Vector3> _positions;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        _transform = transform;
    }

    public void CreateObject(Vector3 createPoint) {
        Instantiate(coinPrefab, createPoint, Quaternion.identity, _transform);
    }

    public void CreateObject(Transform waterArea, int coinCount) {
        FillPositionList(waterArea);
        if (coinCount > _positions.Count) coinCount = _positions.Count;
        for (int i = 0; i < coinCount; i++) {
            int position = Random.Range(0, _positions.Count);
            Vector3 coinPosition = _positions[position];
            _positions.RemoveAt(position);
            CreateObject(coinPosition);
        }
    }

    private void FillPositionList(Transform waterArea) {
        Bounds boundsArea = waterArea.GetComponent<Renderer>().bounds;
        Bounds boundsObject = coinPrefab.GetComponent<Renderer>().bounds;
        float maxObjectSize = Mathf.Max(boundsObject.size.x, boundsObject.size.z);
        int x = (int)(boundsArea.size.x / maxObjectSize);
        int z = (int)(boundsArea.size.z / maxObjectSize - 4);
        _positions = new List<Vector3>();
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < z; j++) {
                float positionX = waterArea.position.x - boundsArea.extents.x + i * maxObjectSize + maxObjectSize / 2;
                float positionZ = 2 * maxObjectSize - boundsArea.extents.z + j * maxObjectSize + maxObjectSize / 2;
                Vector3 position = new Vector3(positionX, .7f, positionZ);
                _positions.Add(position);
            }
        }
    }
}
