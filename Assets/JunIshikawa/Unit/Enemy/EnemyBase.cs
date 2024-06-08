using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBase : EntityBase
{
    //protected int enemyCurrentHp;
    //protected int enemyMaxHp;

    protected int enemyAttackPoint;
    protected int enemyMoveSpeed;

    //public event Action<EnemyBase> onDestroyEvent;
    public event Action<Collision, EnemyBase> onCollideEvent;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    public virtual void Init(int _enemyMaxHp, int _enemyAttackPoint)
    {
        entityMaxHp = _enemyMaxHp;
        enemyAttackPoint = _enemyAttackPoint;

        entityCurrentHp = entityMaxHp;

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void NavMeshDestinationSet(Vector3 _destinationPos)
    {
        navMeshAgent.destination = _destinationPos;
    }

    public void NavMeshAgentIsStopped(bool _isStopped)
    {
        navMeshAgent.isStopped = _isStopped;
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

    private void OnCollisionEnter(Collision _collision)
    {
        if (onCollideEvent == null) return;
        onCollideEvent?.Invoke(_collision, this);
    }
}
