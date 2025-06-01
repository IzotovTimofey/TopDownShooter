using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyController : GameplayEntity
{
    [SerializeField] private EnemyMovementComponent _enemyMovementComponent;
    [SerializeField] private EnemyShootingComponent _enemyShootingComponent;

    // Патрулирование между точками
    // Движение за игроком
    // Обнаружение игрока
    // Поворот в сторону цели
    // Стрельба

    protected override void OnEnable()
    {
        _enemyMovementComponent.PlayerInRange += ShootPlayer;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _enemyMovementComponent.PlayerInRange -= ShootPlayer;
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void ShootPlayer()
    {
        if (_enemyMovementComponent.InRange)
        {
            _enemyShootingComponent.Shoot();
        }
        else
        {
            _enemyShootingComponent.StopShooting();
        }
    }
}