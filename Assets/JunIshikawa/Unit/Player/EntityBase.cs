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

    public virtual void EntityComponentSetUp()
    {
        entityRigidbody = GetComponent<Rigidbody>();
        entityAnimator = GetComponent<Animator>();
    }

    public virtual void EntityHeal(int _healPoint)
    {
        entityCurrentHp += _healPoint;

        if(entityCurrentHp > entityMaxHp)
        {
            entityCurrentHp = entityMaxHp;
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
