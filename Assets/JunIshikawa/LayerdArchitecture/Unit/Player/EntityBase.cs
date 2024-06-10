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

    public event Action<EntityBase> onEntityDestroyEvent;

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

    //public virtual void EntityHeal(int _healPoint)
    //{
    //    entityCurrentHp += _healPoint;

    //    if(entityCurrentHp > entityMaxHp)
    //    {
    //        entityCurrentHp = entityMaxHp;
    //    }
    //}

    public int GetEntityHp
    {
        get
        {
            return entityCurrentHp;
        }
    }

    public virtual void EntityGetDamage(int _damagePoint)
    {
        entityCurrentHp -= _damagePoint;
    }

    public virtual void EntityDestroy()
    {
        Destroy(this.gameObject);
    }
}
