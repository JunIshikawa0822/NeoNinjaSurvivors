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
        //�e�ۂ𐶐�
        Bullet bulletInstance = GameObject.Instantiate(gameStat.bullet,gameStat.player.transform.position,Quaternion.identity);
        bulletInstance.Init(gameStat.attackVector, gameStat.bulletSpeed, gameStat.maxDistance);

        //�e�ۂ̃��X�g�ɒǉ�
        gameStat.bulletList.Add(bulletInstance);
    }
}
