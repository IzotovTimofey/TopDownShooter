using UnityEngine;

public class PlayerDirectionProvider : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    private Vector2 _moveDirection;
    private Vector3 _mousePositionV3;
    private Vector3 _idleDashDirection;
    private Quaternion _mouseLookAngle;

    public Vector2 MoveDirection => _moveDirection;
    public Vector3 MousePositionV3 => _mousePositionV3;
    public Vector3 IdleDashDirection => _idleDashDirection;
    public Quaternion MouseLookAngle => _mouseLookAngle;

    private void Update()
    {
       GetDirection(); 
    }

    private void GetDirection()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 mousePosV3 = mouseScreenPosition - transform.position;
        _mousePositionV3 = mousePosV3;

        float angle = Mathf.Atan2(mousePosV3.y, mousePosV3.x) * Mathf.Rad2Deg;
        _mouseLookAngle = Quaternion.Euler(new Vector3(0,0, angle));

        mousePosV3.z = 0;
        _idleDashDirection = mousePosV3.normalized;

        _moveDirection = _reader.MoveInput.normalized;
    }
}
