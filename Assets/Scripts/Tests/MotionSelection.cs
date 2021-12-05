using UnityEngine;
using UnityEngine.UI;

public class MotionSelection : MonoBehaviour
{
    [SerializeField] private MovingSystem _physicalSystem;
    [SerializeField] private NonPhysicalMovement _nonPhysicalSystem;
    [SerializeField] private Text _buttonText;

    private bool _isPhysicalMovement;

    private void Start() {
        _physicalSystem.enabled = true;
        _nonPhysicalSystem.enabled = false;
        _isPhysicalMovement = true;
        _buttonText.text = "Physics";
    }

    public void ChangeMovingSystem() {
        _isPhysicalMovement = !_isPhysicalMovement;
        if (_isPhysicalMovement) {
            _physicalSystem.enabled = true;
            _nonPhysicalSystem.enabled = false;
            _buttonText.text = "Physics";
        } else {
            _physicalSystem.enabled = false;
            _nonPhysicalSystem.enabled = true;
            _buttonText.text = "Non Physics";
        }
    }


}
