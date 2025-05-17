using UnityEngine;

public class PlayerDirectionSetter : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    public Vector3 LookingDirection()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(_reader.LookInput);
        Vector3 targetDirection = mouseScreenPosition - transform.position;
        targetDirection.z = 0;
        targetDirection = targetDirection.normalized;
        return targetDirection;
    }
    
    public Vector2 WalkingDirection()
    {
        Vector2 moveDirection = _reader.MoveInput.normalized;
        return moveDirection;
    }
}
