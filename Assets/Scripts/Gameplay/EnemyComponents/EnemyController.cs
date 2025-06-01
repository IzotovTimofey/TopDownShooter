using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyController : GameplayEntity // TODO: К чему тут название "Controller"? Избегай в названиях слов Manager, Controller, и.т.п
{
    [SerializeField] private EnemyMovementComponent _enemyMovementComponent;
    [SerializeField] private EnemyShootingComponent _enemyShootingComponent;

    // ?????????????? ????? ???????
    // ???????? ?? ???????
    // ??????????? ??????
    // ??????? ? ??????? ????
    // ????????

    protected override void OnEnable()
    {
        _enemyMovementComponent.PlayerInRange += ShootPlayer; // TODO: Каким боком MOVEMENT компонент, сигналит о том, что игрок в зоне. Это его зона ответственности?
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
        if (_enemyMovementComponent.InRange) // TODO: Идентично, не зона онтветственности мувера 
        {
            
            //TODO: Нет никакой проверки на то, что мы УЖЕ стреляем (внутри класса стрельбы). У тебя вызов события что игрок в зоне происходит в UPDATE (!!!!),
            // из-за чего далее ты кажды кадр стартуешь новую корутину.
            // Эвент сигнализирует о начале и конце действия, точно не в Update
            _enemyShootingComponent.Shoot(); 
        }
        else
        {
            _enemyShootingComponent.StopShooting();
        }
    }
}