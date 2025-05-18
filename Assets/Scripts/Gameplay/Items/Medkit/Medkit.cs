using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int _healingValue = 25;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlayerController controller))
        {
            OnPickUp(controller.HealthComponent);
            gameObject.SetActive(false);
        }

    }

    private void OnPickUp(HealthComponent health)
    {
        health.Heal(_healingValue);
    }
    
}
