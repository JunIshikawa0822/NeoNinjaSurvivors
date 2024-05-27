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
    private int bulletDamage;//弾丸のダメージ量
    private int penetrateCount;//貫通可能回数



    //弾丸がListから削除されるAction
    public event Action<Bullet> bulletRemoveEvent;
    public event Action<Collision> bulletCollideEvent;

    public void Init(Vector3 _moveDir , float _bulletSpeed , float _maxDistance , int _bulletDamage , int _penetrateCount)
    {
        //attackVectorが代入される
        this.moveDir = _moveDir;
        this.bulletSpeed = _bulletSpeed;
        this.maxDistance = _maxDistance;
        this.bulletDamage = _bulletDamage;
        this.penetrateCount = _penetrateCount;
    }

    public void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(moveDistance > maxDistance)
        {
            OnBulletRemove();//リストから削除
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

    //弾丸が消えるときに起きるイベント
    private void OnBulletRemove()
    {
        if (bulletRemoveEvent == null) return;
        bulletRemoveEvent?.Invoke(this);  
    }

    //弾丸が破壊
    private void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    //弾丸の衝突
    private void OnCollisionEnter(Collision _collision)
    {
       // if (bulletCollideEvent == null) return;
        if (bulletRemoveEvent == null) return;

        if(_collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = _collision.gameObject.GetComponent<EnemyBase>();
            if(enemy == null) return;
            //敵のダメージ関数を起動
            enemy.TakeDamage(bulletDamage);
            //ぶつかったときのエフェクトとか起こす
            bulletCollideEvent?.Invoke(_collision);
            //弾丸の貫通可能回数を１減らす
            penetrateCount--;
            //これ以上貫通できる場合ここでreturn
            if(penetrateCount > 0) return;
            
            OnBulletRemove();//リストから削除
            BulletDestroy();//オブジェクトを破壊
        }
        else if(_collision.gameObject.CompareTag("Wall"))
        {
            //壁にぶつかったときの破壊

            OnBulletRemove();//リストから削除
            BulletDestroy();//オブジェクトを破壊
        }
        
    }
    
}
