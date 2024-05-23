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
        //eΫπΆ¬
        Bullet bulletInstance = GameObject.Instantiate(gameStat.bullet,gameStat.player.transform.position,Quaternion.identity);
        bulletInstance.Init(gameStat.attackVector, gameStat.bulletSpeed, gameStat.maxDistance);
        //ActionΙeΫπjόEν·ιΦ
        bulletInstance.bulletDestroyEvent += BulletDestroyAndRemove;
        //eΫΜXgΙΗΑ
        gameStat.bulletList.Add(bulletInstance);
    }

    //ΑθΜeΫπjό΅Xg©ην
    private void BulletDestroyAndRemove(Bullet _bullet)
    {
        //eΫΙA^b`³κ½©
        _bullet.OnDestroy();
        gameStat.bulletList.Remove(_bullet);
    }
}
