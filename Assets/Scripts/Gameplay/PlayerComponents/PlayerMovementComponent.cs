using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private InputReader _reader;

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        float ScaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 move = new Vector2(_reader.MoveInput.x, _reader.MoveInput.y).normalized;
        transform.position += move * ScaledMoveSpeed;
    }

    private void Look()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 targetDirection = mouseScreenPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
