
using System;
using UnityEngine;

public class Player : GameplayEntity
{
    [SerializeField] private PlayerInteractor _interactor;
    
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        _interactor.GetPlayerReference(gameObject);
    }
}
