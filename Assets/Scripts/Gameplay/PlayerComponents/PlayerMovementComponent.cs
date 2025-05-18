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
        Vector2 moveDirection = _directionProvider.GetWalkDirection();
        _rb2D.linearVelocity = moveDirection * _moveSpeed;
    }

    private void Look()
    {
        Vector3 lookDirection = _directionProvider.GetLookDirectionVector();
        if (lookDirection.magnitude > _turningThreshold)
           transform.rotation = _directionProvider.GetLookDirection();
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
        Vector2 moveDirection = _directionProvider.GetWalkDirection();
        Vector3 targetDirection = _directionProvider.GetIdleDashDirection();

        _rb2D.linearVelocity = moveDirection == Vector2.zero ? targetDirection * _dashForceValue : moveDirection * _dashForceValue;

        _canDash = false;
        _isDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}
