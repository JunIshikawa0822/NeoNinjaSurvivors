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
        //íeä€Çê∂ê¨
        Bullet bulletInstance = GameObject.Instantiate(gameStat.bullet,gameStat.player.transform.position,Quaternion.identity);
        bulletInstance.Init(gameStat.attackVector, gameStat.bulletSpeed, gameStat.maxDistance);

        //íeä€ÇÃÉäÉXÉgÇ…í«â¡
        gameStat.bulletList.Add(bulletInstance);
    }
}
