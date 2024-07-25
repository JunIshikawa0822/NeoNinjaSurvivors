using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float bulletSpeed;
    private int bulletDamage;//弾丸のダメージ量
    private int penetrateCount;//貫通可能回数

    //弾丸がListから削除されるAction
    public event Action<Bullet> bulletRemoveEvent;
    //
    public event Action<Collision , Bullet> bulletCollideEvent;

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
        if (moveDistance > maxDistance)
        {
            OnTriggerNextAction();//リストから削除
            BulletDestroy();//オブジェクトを破壊
        }
        else
        {
            Move();
        }
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

    //弾丸が消えるときに起きるイベント
    public void OnTriggerNextAction()
    {
        if (bulletRemoveEvent == null) return;
        bulletRemoveEvent?.Invoke(this);  
    }

    //弾丸が破壊
    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    //弾丸の衝突
    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("衝突");
        if (bulletCollideEvent == null) return;
        bulletCollideEvent?.Invoke(_collision, this);
    }

    private void RotateSet(Vector3 _directionVec)
    {
        Vector3 angles = transform.localEulerAngles;
        float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
        angles.y = -angle - 180;
        transform.localEulerAngles = angles;
    }

    //private float VecToAngle(Vector3 _directionVec)
    //{
    //    float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
    //    Debug.Log(angle);
    //    return angle;
    //}
}
