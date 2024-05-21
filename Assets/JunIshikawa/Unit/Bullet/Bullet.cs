using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float bulletSpeed;

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
        Vector3 moveValue = moveDir * bulletSpeed;
        transform.position += moveValue;
        //moveDistance += moveValue.magnitude;
    }
}
