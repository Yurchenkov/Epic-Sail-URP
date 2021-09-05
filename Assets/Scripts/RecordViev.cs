using UnityEngine;
using UnityEngine.UI;

public class RecordViev : MonoBehaviour {

    public RectTransform linePrefab;
    public RectTransform content;

    public void ShowRecordList() {

        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

        foreach (var line in GameManager.instance.playerData.scoreList) {
            RectTransform lineItem = Instantiate(linePrefab);
            lineItem.transform.SetParent(content, false);
            lineItem.GetComponent<Text>().text = line.Value;
        }
    }
}
