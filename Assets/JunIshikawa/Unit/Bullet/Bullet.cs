using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float bulletSpeed;

    //’eŠÛ‚ª”j‰ó‚³‚ê‚éAction
    public event Action<Bullet> bulletDestroyEvent;
    //public event Action<Collision> bulletCollideEvent;

    public void Init(Vector3 _moveDir , float _bulletSpeed , float _maxDistance)
    {
        //attackVector‚ª‘ã“ü‚³‚ê‚é
        this.moveDir = _moveDir;
        this.bulletSpeed = _bulletSpeed;
        this.maxDistance = _maxDistance;
    }

    public void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(moveDistance > maxDistance)
        {
            if (bulletDestroyEvent == null) return;
            bulletDestroyEvent?.Invoke(this);
        }
        else
        {
            Vector3 moveValue = moveDir * bulletSpeed;
            transform.position += moveValue;
            moveDistance += moveValue.magnitude;
        }    
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision _collision)
    {
       // if (bulletCollideEvent == null) return;
        if (bulletDestroyEvent == null) return;
       // bulletCollideEvent?.Invoke(_collision);
        bulletDestroyEvent?.Invoke(this);
    }
}
