using UnityEngine;

public class Bullet : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GameplayEntity controller))
        {
            DealDamage(controller.Health);
        }
        else if (collision.gameObject.tag == "Map")
        {
            Release();
        }
    }
}   