using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;

    private Vector2 _moveInput;
    private Vector2 _lookInput;

    public Vector2 MoveInput => _moveInput;
    public Vector2 LookInput => _lookInput;

    private void OnEnable()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Enable();

        _inputSystem.Player.Move.performed += OnMove;
        _inputSystem.Player.Move.canceled += OnMove;

        _inputSystem.Player.Look.performed += OnLook;
        _inputSystem.Player.Look.canceled += OnLook;

    }

    private void OnDisable()
    {
        _inputSystem.Player.Move.performed -= OnMove;
        _inputSystem.Player.Move.canceled -= OnMove;

        _inputSystem.Player.Look.performed -= OnLook;
        _inputSystem.Player.Look.canceled -= OnLook;

        _inputSystem.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    private void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

}
