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

    public void CreateObject(Vector3 creationPoint) {
        Instantiate(coinPrefab, creationPoint, Quaternion.identity, _transform);
    }

    public void CreateObject(Transform waterArea, int coinCount) {
        FillPositionList(waterArea);
        if (coinCount > _positions.Count)
            coinCount = _positions.Count;

        for (int i = 0; i < coinCount; i++) {
            int position = Random.Range(0, _positions.Count);
            Vector3 coinPosition = _positions[position];
            _positions.RemoveAt(position);
            CreateObject(coinPosition);
        }
    }

    private void FillPositionList(Transform waterArea) {
        Bounds areaBounds = waterArea.GetComponent<Renderer>().bounds;
        Bounds objectBounds = coinPrefab.GetComponent<Renderer>().bounds;
        _positions = new List<Vector3>();
        float maxObjectSize = Mathf.Max(objectBounds.size.x, objectBounds.size.z);
        int x = (int)(areaBounds.size.x / maxObjectSize);
        int z = (int)(areaBounds.size.z / maxObjectSize - areaBounds.size.z * .3f);

        for (int i = 0; i < x; i++) {
            for (int j = 0; j < z; j++) {
                float positionX = waterArea.position.x - areaBounds.extents.x + i * maxObjectSize + maxObjectSize / 2;
                float positionZ = 2 * maxObjectSize - areaBounds.extents.z + j * maxObjectSize + maxObjectSize / 2;
                Vector3 position = new Vector3(positionX, .7f, positionZ);
                _positions.Add(position);
            }
        }
    }
}
