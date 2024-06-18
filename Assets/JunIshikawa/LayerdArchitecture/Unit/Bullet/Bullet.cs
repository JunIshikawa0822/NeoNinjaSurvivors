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

    public void Init(Vector3 _moveDir , float _bulletSpeed , float _maxDistance , int _bulletDamage , int _penetrateCount, float _angle)
    {
        //attackVectorが代入される
        this.moveDir = _moveDir;
        this.bulletSpeed = _bulletSpeed;
        this.maxDistance = _maxDistance;
        this.bulletDamage = _bulletDamage;
        this.penetrateCount = _penetrateCount;

        Vector3 angles = transform.localEulerAngles;
        angles.y = -_angle - 180;
        transform.localEulerAngles = angles;
    }

    public void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(moveDistance > maxDistance)
        {
            OnTriggerNextAction();//リストから削除
            BulletDestroy();//オブジェクトを破壊
        }
        else
        {
            Vector3 moveValue = moveDir * bulletSpeed;
            transform.position += moveValue;
            moveDistance += moveValue.magnitude;
        }    
    }

    //弾丸のダメージを返す
    public int BulletDamage()
    {
        return bulletDamage;
    }
    //弾丸の貫通可能回数を返す
    public int PenetrateCount
    {
        get{
            return penetrateCount;
        }
        set{
            if(value >= 0) penetrateCount = value;
        }
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
        Debug.Log("衝突してはいるのよ");
        if (bulletCollideEvent == null) return;
        bulletCollideEvent?.Invoke(_collision , this);
    }
}
