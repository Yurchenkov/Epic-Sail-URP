using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour {

    public Animator transition;

    private float _transitionTime = 2f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER))
            LoadNextLevel(Constants.BUILD_INDEX_INFINITE_LEVEL);       
    }

    public void LoadNextLevel(int levelIndex) {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger(Constants.ANIMATION_TRIGGER_CROSSFADE);

        yield return new WaitForSeconds(_transitionTime);
        GameManager.instance.playerData.ResetLevelMoney();
        SaveLoadManager.Save(GameManager.instance.playerData);
        SceneManager.LoadScene(levelIndex);
    }
}
