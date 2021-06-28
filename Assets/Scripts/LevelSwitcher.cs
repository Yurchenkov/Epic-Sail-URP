using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour {

    public Animator transition;

    private float _transitionTime = 2f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.TAG_PLAYER))
            LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void LoadNextLevel(int levelIndex) {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger(Constants.ANIMATION_TRIGGER_CROSSFADE);

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
