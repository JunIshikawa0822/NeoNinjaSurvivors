using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    protected int entityCurrentHp;
    protected int entityMaxHp;

    protected Rigidbody entityRigidbody;
    protected Animator entityAnimator;
    [SerializeField] private DamageFXUpdater enemy_DamageFXUpdater;
    void Start()
    {
        if(!enemy_DamageFXUpdater)
        {
            enemy_DamageFXUpdater = GetComponentInChildren<DamageFXUpdater>();
        }
    }

    public virtual void EntityHpSetUp(int _enemyMaxHp)
    {
        entityMaxHp = _enemyMaxHp;

        entityCurrentHp = entityMaxHp;
    }

    public virtual void EntityComponentSetUp()
    {
        entityRigidbody = GetComponent<Rigidbody>();
        entityAnimator = GetComponentInChildren<Animator>();
    }

    public int GetEntityHp
    {
        get
        {
            return entityCurrentHp;
        }
    }

    public virtual void EntityGetDamage(int _damagePoint)
    {
        if(enemy_DamageFXUpdater)
        {
            enemy_DamageFXUpdater.InitializeFlash();
        }
        entityCurrentHp -= _damagePoint;
    }
}