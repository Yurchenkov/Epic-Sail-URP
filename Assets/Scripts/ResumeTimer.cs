using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResumeTimer : MonoBehaviour {
    public delegate void Timer();
    public event Timer CountdownIsOver;

    [SerializeField] private GameObject _timerCanvas;
    [SerializeField] private Animator _timerAnimator;

    private void Start() {
        _timerAnimator = GetComponent<Animator>();
    }

    public void StartTimer() {
        _timerCanvas.SetActive(true);
        _timerAnimator.SetTrigger(Constants.ANIMATION_TRIGGER_CROSSFADE);
    }

    public void CloseTimer() {
        CountdownIsOver?.Invoke();
        _timerCanvas.SetActive(false);
    }
}
