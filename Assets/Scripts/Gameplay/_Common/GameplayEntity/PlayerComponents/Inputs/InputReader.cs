using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;

    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _scrollValue;

    public Vector2 MoveInput => _moveInput;
    public Vector2 LookInput => _lookInput;
    public event UnityAction OnPlayerDashInput;
    public event UnityAction<bool> OnPlayerShoot;
    public event UnityAction OnPlayerReload;
    public event UnityAction<float> OnPlayerWeaponSwap;

    private void OnEnable()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Enable();

        _inputSystem.Player.Move.performed += OnMove;
        _inputSystem.Player.Move.canceled += OnMove;

        _inputSystem.Player.Look.performed += OnLook;
        _inputSystem.Player.Look.canceled += OnLook;

        _inputSystem.Player.Dash.performed += OnDash;
        _inputSystem.Player.Dash.canceled += OnDash;

        _inputSystem.Player.Shoot.performed += OnShoot;
        _inputSystem.Player.Shoot.canceled += OnShoot;

        _inputSystem.Player.Reload.performed += OnReload;

        _inputSystem.Player.SwapWeapon.performed += OnWeaponSwap;
    }

    private void OnDisable()
    {
        _inputSystem.Player.Move.performed -= OnMove;
        _inputSystem.Player.Move.canceled -= OnMove;

        _inputSystem.Player.Look.performed -= OnLook;
        _inputSystem.Player.Look.canceled -= OnLook;

        _inputSystem.Player.Dash.performed -= OnDash;
        _inputSystem.Player.Dash.canceled -= OnDash;

        _inputSystem.Player.Shoot.performed -= OnShoot;
        _inputSystem.Player.Shoot.canceled -= OnShoot;

        _inputSystem.Player.Reload.performed -= OnReload;

        _inputSystem.Player.SwapWeapon.performed -= OnWeaponSwap;

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

    private void OnDash(InputAction.CallbackContext context)
    {
        OnPlayerDashInput?.Invoke();
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        var state = context.canceled ? false : true; // TODO: упростить
        OnPlayerShoot?.Invoke(state);
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        OnPlayerReload?.Invoke();
    }

    private void OnWeaponSwap(InputAction.CallbackContext context)
    {
        _scrollValue = context.ReadValue<float>();
        OnPlayerWeaponSwap?.Invoke(_scrollValue);
    }
}
