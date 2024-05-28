using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //public event Action<EnemyBase> enemyDead;
    public void TakeDamage(int _bulletDamage)
    {   
        Debug.Log("被弾した！　ダメージ：" + _bulletDamage);
    }
}
