using System.Collections;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float _dashForceValue = 10f;
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private float _dashCooldown = 1f;

    [SerializeField] private InputReader _reader;

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
        Vector2 moveDirection = new Vector2(_reader.MoveInput.x, _reader.MoveInput.y).normalized;
        _rb2D.linearVelocity = new Vector2(moveDirection.x * _moveSpeed, moveDirection.y * _moveSpeed);
    }

    private void Look()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 targetDirection = mouseScreenPosition - transform.position;
        if (targetDirection.magnitude > 10.01)
        {
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
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
        Vector2 moveDirection = new Vector2(_reader.MoveInput.x, _reader.MoveInput.y).normalized;
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            _canDash = false;
            _isDashing = true;
            _rb2D.linearVelocity = new Vector2(moveDirection.x * _dashForceValue, moveDirection.y * _dashForceValue);
            yield return new WaitForSeconds(_dashDuration);
            _isDashing = false;

            yield return new WaitForSeconds(_dashCooldown);
            _canDash = true;
        }
    }
}
