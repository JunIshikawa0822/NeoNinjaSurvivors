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
            BulletInstantiate();
        }
    }

    private void BulletInstantiate()
    {
        //弾丸を生成
        Bullet bulletInstance = GameObject.Instantiate(gameStat.bullet,gameStat.player.transform.position,Quaternion.identity);
        bulletInstance.Init(gameStat.attackVector, gameStat.bulletSpeed, gameStat.maxDistance);
        //Actionに弾丸を破棄・削除する関数を代入
        bulletInstance.bulletDestroyEvent += BulletDestroyAndRemove;
        //弾丸のリストに追加
        gameStat.bulletList.Add(bulletInstance);
    }

    //特定の弾丸を破棄しリストから削除
    private void BulletDestroyAndRemove(Bullet _bullet)
    {
        //弾丸にアタッチされた自爆処理
        _bullet.OnDestroy();
        gameStat.bulletList.Remove(_bullet);
    }
}
