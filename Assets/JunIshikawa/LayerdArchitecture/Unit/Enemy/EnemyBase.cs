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
    public event Action<EnemyBase> enemyRemoveEvent;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    public virtual void Init(int _enemyAttackPoint)
    {
        enemyAttackPoint = _enemyAttackPoint;

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

    public void OnTriggerNextAction()
    {
        if (enemyRemoveEvent == null) return;
        enemyRemoveEvent?.Invoke(this);
    }

    public int GetEnemyAttack
    {
        get
        {
            return enemyAttackPoint;
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (onCollideEvent == null) return;
        onCollideEvent?.Invoke(_collision, this);
    }
}
