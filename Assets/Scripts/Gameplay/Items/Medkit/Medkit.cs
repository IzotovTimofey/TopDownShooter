using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int _healingValue = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            OnPickUp(controller.HealthComponent);
        }
    }

    private void OnPickUp(HealthComponent health)
    {
        health.Heal(_healingValue);
        gameObject.SetActive(false);
    }
    
}
