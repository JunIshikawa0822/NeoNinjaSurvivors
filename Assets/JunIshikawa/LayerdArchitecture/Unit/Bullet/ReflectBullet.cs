using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBullet : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float bulletSpeed;
    private int bulletDamage;//弾丸のダメージ量

    //弾丸がListから削除されるAction
    public event Action<ReflectBullet> reflectBulletRemoveEvent;
    //ぶつかった時
    public event Action<ReflectBullet, Collision> reflectBulletCollideEvent;
    //反射
    public event Action<ReflectBullet, Collision> reflectEvent;

    public void Init(Vector3 _moveDir, float _bulletSpeed, float _maxDistance, int _bulletDamage)
    {
        //attackVectorが代入される
        this.moveDir = _moveDir;
        this.bulletSpeed = _bulletSpeed;
        this.maxDistance = _maxDistance;
        this.bulletDamage = _bulletDamage;

        RotateSet(moveDir);
    }

    public void OnUpdate()
    {
        //Debug.Log("OnUpdate");
        if (moveDistance > maxDistance)
        {
            OnTriggerNextAction();//リストから削除
            BulletDestroy();//オブジェクトを破壊
        }
        else
        {
            Move();
            Reflect(null);
        }
    }

    private void Move()
    {
        Vector3 moveValue = moveDir * bulletSpeed;
        transform.position += moveValue;
        moveDistance += moveValue.magnitude;
    }

    public Vector3 GetSetVector
    {
        get { return this.moveDir; }
        set { this.moveDir = value; }
    }

    //弾丸のダメージを返す
    public int BulletDamage()
    {
        return bulletDamage;
    }

    //弾丸が消えるときに起きるイベント
    private void OnTriggerNextAction()
    {
        if (reflectBulletRemoveEvent == null) return;
        reflectBulletRemoveEvent?.Invoke(this);
    }

    //弾丸が破壊
    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    //弾丸の衝突
    private void OnCollisionEnter(Collision _collision)
    {
        if (reflectBulletCollideEvent == null) return;
        reflectBulletCollideEvent?.Invoke(this, _collision);
    }

    private void RotateSet(Vector3 _directionVec)
    {
        Vector3 angles = transform.localEulerAngles;
        float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
        angles.y = -angle - 180;
        transform.localEulerAngles = angles;
    }

    private void Reflect(Collision _collision)
    {
        if (reflectEvent == null) return;
        reflectEvent?.Invoke(this, _collision);

        RotateSet(moveDir);
    }
}
