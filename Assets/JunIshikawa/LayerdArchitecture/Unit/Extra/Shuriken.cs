using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuriken : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float bulletSpeed;
    private int bulletDamage;//弾丸のダメージ量
    private int penetrateCount;//貫通可能回数

    //手裏剣がListから削除されるAction
    public event Action<Shuriken> ShurikenRemoveEvent;
    //ぶつかった時
    public event Action<Collision, Shuriken> ShurikenCollideEvent;

    public void Init(Vector3 _moveDir , float _bulletSpeed , float _maxDistance , int _bulletDamage , int _penetrateCount)
    {
        //attackVectorが代入される
        this.moveDir = _moveDir;
        this.bulletSpeed = _bulletSpeed;
        this.maxDistance = _maxDistance;
        this.bulletDamage = _bulletDamage;
        this.penetrateCount = _penetrateCount;

        RotateSet(moveDir);
    }

    public void OnUpdate()
    {
        
    }

    private void Move()
    {
        Vector3 moveValue = moveDir * bulletSpeed;
        transform.position += moveValue;
        moveDistance += moveValue.magnitude;
    }

    //弾丸のダメージを返す
    public int BulletDamage()
    {
        return bulletDamage;
    }
    //弾丸の貫通可能回数を返す
    public int PenetrateCount
    {
        get{ return penetrateCount; }
        set{ if(value >= 0) penetrateCount = value; }
    }

    //手裏剣が消えるときに起きるイベント
    public void OnTriggerNextAction()
    {
        if (ShurikenRemoveEvent == null) return;
        ShurikenRemoveEvent?.Invoke(this);  
    }

    //手裏剣が破壊
    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    //手裏剣の衝突
    private void OnCollisionEnter(Collision _collision)
    {
        if (ShurikenCollideEvent == null) return;
        ShurikenCollideEvent?.Invoke(_collision , this);
    }

    private void RotateSet(Vector3 _directionVec)
    {
        Vector3 angles = transform.localEulerAngles;
        float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
        angles.y = -angle - 180;
        transform.localEulerAngles = angles;
    }
}
