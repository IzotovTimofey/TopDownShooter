using UnityEngine;

public class PlayerDirectionProvider : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    public Vector2 MoveDirection { get; private set; }
    public Vector3 DirectionToMouse { get; private set; }
    public Vector3 IdleDashDirection { get; private set; }
    public Quaternion MouseLookAngle { get; private set; }

    private void Update()
    {
       GetDirection(); 
    }

    private void GetDirection()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 directionToMouse = mouseScreenPosition - transform.position;
        DirectionToMouse = directionToMouse;

        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        MouseLookAngle = Quaternion.Euler(new Vector3(0,0, angle));

        directionToMouse.z = 0;
        IdleDashDirection = directionToMouse.normalized;

        MoveDirection = _reader.MoveInput.normalized;
    }
}
