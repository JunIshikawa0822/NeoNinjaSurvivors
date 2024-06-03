using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    protected int enemyCurrentHp;
    protected int enemyMaxHp;

    protected int enemyAttackPoint;
    protected int enemyMoveSpeed;

    protected event Action<EnemyBase> onDestroyEvent;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;
    protected Rigidbody enemyRigidbody;

    public virtual void Init(int _enemyMaxHp, int _enemyAttackPoint)
    {
        enemyMaxHp = _enemyMaxHp;
        enemyAttackPoint = _enemyAttackPoint;

        enemyCurrentHp = enemyMaxHp;
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void EnemyMove()
    {

    }

    public void NavMeshDestinationSet(Vector3 _destinationPos)
    {
        navMeshAgent.destination = _destinationPos;
    }

    public void EnemyDead(EnemyBase _enemy)
    {
        Destroy(_enemy.gameObject);
    }

    public void EnemyGetDamage(EnemyBase _enemy, int _damage)
    {
        _enemy.enemyCurrentHp -= _damage;
    }

    public int SetGetEnemyAttack
    {
        get
        {
            return enemyAttackPoint;
        }
    }

    //public event Action<EnemyBase> enemyDead;
    public void TakeDamage(int _bulletDamage)
    {   
        Debug.Log("被弾した！　ダメージ：" + _bulletDamage);
    }
}
