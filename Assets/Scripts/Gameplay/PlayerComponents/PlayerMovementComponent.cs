using System.Collections;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float _dashForceValue = 10f;
    [SerializeField] private float _dashDuration = 0.1f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private float _turningThreshold = 10.01f;

    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;

    private Rigidbody2D _rb2D;
    private bool _isDashing = false;
    private bool _canDash = true;

    private void OnEnable()
    {
        _reader.OnPlayerDashInput += Dash;
    }

    private void OnDisable()
    {
        _reader.OnPlayerDashInput -= Dash;
    }

    private void Start()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
            Move();
        Look();
    }

    private void Move()
    {
        _rb2D.linearVelocity = _directionProvider.MoveDirection * _moveSpeed;
    }

    private void Look()
    {
        if (_directionProvider.MousePositionV3.magnitude > _turningThreshold)
            transform.rotation = _directionProvider.MouseLookAngle;
    }
    private void Dash()
    {
        if (_isDashing)
            return;
        if (_canDash)
            StartCoroutine(PerformingDash());
    }

    private IEnumerator PerformingDash()
    {
        _rb2D.linearVelocity = _directionProvider.MoveDirection == Vector2.zero ? _directionProvider.IdleDashDirection * _dashForceValue : _directionProvider.MoveDirection * _dashForceValue;

        _canDash = false;
        _isDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}
