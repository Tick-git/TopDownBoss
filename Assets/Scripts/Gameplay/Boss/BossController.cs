using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossVisuals _visuals;

    private InputAction _testAction;
    private bool _testIsRotated = true;
    
    private void Awake()
    {
        _testAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (_testAction.WasPressedThisFrame())
        {
            _testIsRotated = !_testIsRotated;
            _visuals.Rotate(_testIsRotated);
        }
    }
}
