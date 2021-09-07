using UnityEngine;
using UnityEngine.UI;

public class RecordView : MonoBehaviour {

    public RectTransform linePrefab;
    public RectTransform content;

    public void ShowRecordList() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

        foreach (var line in RecordTable.GetRecordTable()) {
            RectTransform lineItem = Instantiate(linePrefab);
            lineItem.transform.SetParent(content, false);
            lineItem.GetComponent<Text>().text = $"Score: {line.score}. " +
                                                 $"Distance: {line.groundCount}. " +
                                                 $"Coins: {line.coinCount}. " +
                                                 $"Obstacle: {line.obstacleCount}. " +
                                                 $"Bonuses: {line.bonusCount}.";
        }
    }
}
