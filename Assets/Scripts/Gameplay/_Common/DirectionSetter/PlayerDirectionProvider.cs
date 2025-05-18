using UnityEngine;

public class PlayerDirectionProvider : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    //TODO: —овместить все методы в один, записывать нужные данные в пол€, дл€ полей сделать свойства на чтение
    public Vector3 GetIdleDashDirection()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 targetDirection = mouseScreenPosition - transform.position;
        targetDirection.z = 0;
        targetDirection = targetDirection.normalized;
        return targetDirection;
    }

    public Vector2 GetWalkDirection()
    {
        Vector2 moveDirection = _reader.MoveInput.normalized;
        return moveDirection;
    }

    public Quaternion GetLookDirection()
    {
        Vector3 targetDirection = GetLookDirectionVector();
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion correctAngle = Quaternion.Euler(new Vector3(0, 0, angle));
        return correctAngle;
    }

    public Vector3 GetLookDirectionVector()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 targetDirection = mouseScreenPosition - transform.position;
        return targetDirection;
    } 
}
