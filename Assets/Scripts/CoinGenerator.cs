using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour{

    public static CoinGenerator instance;

    public GameObject coinPrefab;

    private Transform _transform;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        _transform = transform;
    }

    public void CreateCoin(Vector3 createPoint) {
        Instantiate(coinPrefab, createPoint,Quaternion.identity, _transform);
    }

    public void CreateCoin(Transform waterArea, int coinCount) {
       Bounds bounds =  waterArea.GetComponent<Renderer>().bounds;
        float deltaZ = bounds.extents.z * .2f;
        float minPositionX = bounds.min.x;
        float minPositionZ = bounds.min.z + deltaZ;
        float maxPositionX = bounds.max.x;
        float maxPositionZ = bounds.max.z - deltaZ;
        for (int i = 0; i < coinCount; i++) {
            Vector3 coinPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 0.7f, Random.Range(minPositionZ, maxPositionZ));
            CreateCoin(coinPosition);
        }
    }
}
