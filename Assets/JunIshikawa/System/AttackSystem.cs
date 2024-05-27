using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : SystemBase, IOnUpdate
{
    public void OnUpdate()
    {
        if(gameStat.bulletList.Count > 0)
        {
            for(int i = gameStat.bulletList.Count - 1; i >= 0; i--)
            {
                gameStat.bulletList[i].OnUpdate();
            }
        }

        if(gameStat.isAttackInput == true)
        {
            BulletInstantiate(
                gameStat.bullet,
                gameStat.player.transform.position,
                gameStat.attackVector, 
                gameStat.bulletSpeed, 
                gameStat.maxDistance, 
                gameStat.bulletDamage, 
                gameStat.penetrateCount,
                gameStat.bulletList);
        }
    }

    private void BulletInstantiate(Bullet _bullet , Vector3 _playerPos , Vector3 _attackVector , float _bulletSpeed , float _maxDistance , int _bulletDamage , int _penetrateCount , List<Bullet> _bulletList)
    {
        //弾丸を生成
        Bullet bulletInstance = GameObject.Instantiate(_bullet,_playerPos,Quaternion.identity);
        bulletInstance.Init(_attackVector, _bulletSpeed, _maxDistance, _bulletDamage , _penetrateCount);
        //Actionに弾丸をListから削除する関数を登録
        bulletInstance.bulletRemoveEvent += BulletRemove;
        //Actionに被弾時に実行する処理を登録
        bulletInstance.bulletCollideEvent += BulletCollide;
        //弾丸のリストに追加
        _bulletList.Add(bulletInstance);
    }

    //弾丸をリストから削除
    private void BulletRemove(Bullet _bullet)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.bulletList.Remove(_bullet);
    }

    //弾丸が敵に当たった時に実行（エフェクトetc...）
    private void BulletCollide(Collision _collision)
    {
        
    }
}
