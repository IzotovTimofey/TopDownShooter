using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyController : GameplayEntity // TODO: � ���� ��� �������� "Controller"? ������� � ��������� ���� Manager, Controller, �.�.�
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
        _enemyMovementComponent.PlayerInRange += ShootPlayer; // TODO: ����� ����� MOVEMENT ���������, �������� � ���, ��� ����� � ����. ��� ��� ���� ���������������?
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
        if (_enemyMovementComponent.InRange) // TODO: ���������, �� ���� ���������������� ������ 
        {
            
            //TODO: ��� ������� �������� �� ��, ��� �� ��� �������� (������ ������ ��������). � ���� ����� ������� ��� ����� � ���� ���������� � UPDATE (!!!!),
            // ��-�� ���� ����� �� ����� ���� ��������� ����� ��������.
            // ����� ������������� � ������ � ����� ��������, ����� �� � Update
            _enemyShootingComponent.Shoot(); 
        }
        else
        {
            _enemyShootingComponent.StopShooting();
        }
    }
}