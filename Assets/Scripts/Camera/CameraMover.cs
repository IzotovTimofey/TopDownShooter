using UnityEngine;
using UnityEngine.UIElements;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    private void Update()
    {
        MoveAfterPlayer();
    }

    private void MoveAfterPlayer()
    {
        transform.position = new Vector3(_controller.transform.position.x, _controller.transform.position.y, -10f);
    }
}
