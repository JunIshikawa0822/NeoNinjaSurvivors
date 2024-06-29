using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBase : EntityBase
{
    protected int enemyExp;
    protected int enemyAttackPoint;
    protected int enemyMoveSpeed;

    public event Action<EnemyBase> onDestroyEnemyEvent;

    //public event Action<EnemyBase> onDestroyEvent;
    public event Action<Collision, EnemyBase> onCollideEvent;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    public virtual void EnemyInit(int _enemyAttackPoint, int _enemyExp)
    {
        enemyAttackPoint = _enemyAttackPoint;
        enemyExp = _enemyExp;

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void OnUpdate()
    {
        if (entityCurrentHp < 1)
        {
            OnDestroyThisEnemy();
        }
    }

    private void OnDestroyThisEnemy()
    {
        if (onDestroyEnemyEvent == null) return;
        onDestroyEnemyEvent?.Invoke(this);
    }

    public void NavMeshDestinationSet(Vector3 _destinationPos)
    {
        navMeshAgent.destination = _destinationPos;
    }

    public void NavMeshAgentIsStopped(bool _isStopped)
    {
        navMeshAgent.isStopped = _isStopped;
    }

    public int GetEnemyAttack
    {
        get
        {
            return enemyAttackPoint;
        }
    }

    public int GetEnemyExp
    {
        get
        {
            return enemyExp;
        }
    }

    public void EnemyObjectDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (onCollideEvent == null) return;
        onCollideEvent?.Invoke(_collision, this);
    }

    //_playerPosと_enemyPosの位置関係からプレイヤーに近づいた敵を遠ざけるノックバックを実装
    public void EnemyNockBack(Vector3 _auraCenterPos,Vector3 _enemyPos,float _knockBackStrength){
        // プレイヤーから敵への方向ベクトルを計算
        Vector3 knockbackDirection = (_enemyPos - _auraCenterPos).normalized;

        // 敵にノックバック力を加える（Rigidbodyを使用している場合）
        Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(knockbackDirection * _knockBackStrength, ForceMode.Impulse);
        }
        else
        {
            // Rigidbodyがない場合は、位置を直接操作
            transform.position += knockbackDirection * _knockBackStrength * Time.deltaTime;
        }
    }
}
