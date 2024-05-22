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
        //Action�ɒe�ۂ�j���E�폜����֐�����
        bulletInstance.bulletDestroyEvent += BulletDestroyAndRemove;
        //�e�ۂ̃��X�g�ɒǉ�
        gameStat.bulletList.Add(bulletInstance);
    }

    //����̒e�ۂ�j�������X�g����폜
    private void BulletDestroyAndRemove(Bullet _bullet)
    {
        //�e�ۂɃA�^�b�`���ꂽ��������
        _bullet.OnDestroy();
        gameStat.bulletList.Remove(_bullet);
    }
}
