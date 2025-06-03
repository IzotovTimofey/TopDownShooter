using UnityEngine;

public class ReferenceProvider : MonoBehaviour
{
    [SerializeField] private BulletsFactory _bulletsFactory;

    public BulletsFactory GetBulletsFactory()
    {
        return _bulletsFactory;
    }
}
