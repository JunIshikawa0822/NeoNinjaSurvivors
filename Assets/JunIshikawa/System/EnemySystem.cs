using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : SystemBase, IOnUpdate
{
    public void OnUpdate()
    {
        if (gameStat.enemyList.Count > 0)
        {
            for (int i = gameStat.enemyList.Count - 1; i >= 0; i--)
            {
                gameStat.enemyList[i].OnUpdate();

                gameStat.enemyList[i].EnemyMove();

                //動かす
                gameStat.enemyList[i].NavMeshDestinationSet(gameStat.player.transform.position);
            }
        }
    }

    private void EyeballEnemyInstantiate(EyeballEnemy _eyeballEnemy, Vector3 _instantiatePos, int _enemyMaxHp, int _enemyAttackPoint, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate(_eyeballEnemy, _instantiatePos, Quaternion.identity);
        enemy.Init(_enemyMaxHp, _enemyAttackPoint);
        _enemyList.Add(enemy);
    }

    private void GreenEnemyInstantiate(GreenEnemy _greenEnemy, Vector3 _instantiatePos, int _enemyMaxHp, int _enemyAttackPoint, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate(_greenEnemy, _instantiatePos, Quaternion.identity);
        enemy.Init(_enemyMaxHp, _enemyAttackPoint);
        _enemyList.Add(enemy);
    }
}
