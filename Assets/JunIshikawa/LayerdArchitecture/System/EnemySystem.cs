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
                if (!gameStat.isLevelUp)
                {
                    gameStat.enemyList[i].OnUpdate();

                    //動かす
                    gameStat.enemyList[i].NavMeshAgentIsStopped(true);
                    gameStat.enemyList[i].NavMeshDestinationSet(gameStat.player.transform.position);
                }
                else
                {
                    gameStat.enemyList[i].NavMeshAgentIsStopped(false);
                }
            }
        }
    }

    private void EyeballEnemyInstantiate(EyeballEnemy _eyeballEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<EyeballEnemy>(_eyeballEnemy, _data.instantiatePos, Quaternion.identity);
        if (enemy == null) return;

        enemy.EntityComponentSetUp();
        enemy.EntityHpSetUp(_data.enemyMaxHp);

        enemy.EnemyInit(_data.enemyAttackPoint, _data.enemyExp);

        enemy.onCollideEvent += EnemyCollide;

        enemy.onDestroyEnemyEvent += GetEnemyExp;
        enemy.onDestroyEnemyEvent += EnemyRemove;
        enemy.onDestroyEnemyEvent += EnemyDestroy;

        _enemyList.Add(enemy);
    }

    private void GreenEnemyInstantiate(GreenEnemy _greenEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<GreenEnemy>(_greenEnemy, _data.instantiatePos, Quaternion.identity);

        if (enemy == null) return;

        enemy.EntityComponentSetUp();
        enemy.EntityHpSetUp(_data.enemyMaxHp);

        enemy.EnemyInit(_data.enemyAttackPoint, _data.enemyExp);

        enemy.onCollideEvent += EnemyCollide;

        enemy.onDestroyEnemyEvent += GetEnemyExp;
        enemy.onDestroyEnemyEvent += EnemyRemove;
        enemy.onDestroyEnemyEvent += EnemyDestroy;

        _enemyList.Add(enemy);
    }

    //敵が何かに当たった時に発動
    private void EnemyCollide(Collision _collision, EnemyBase _enemy)
    {
        if(!_collision.transform.CompareTag("Player"))return;

        Player player = _collision.transform.GetComponent<Player>();
        if (player == null) return;

        //プレイヤーのダメージ関数を起動
        player.EntityGetDamage(_enemy.GetEnemyAttack);

        if (_enemy.GetEntityHp > 0) return;
    }

    private void EnemyRemove(EnemyBase _enemy)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.enemyList.Remove(_enemy);
    }

    private void GetEnemyExp(EnemyBase _enemy)
    {
        gameStat.playerTotalExp += _enemy.GetEnemyExp;
    }

    private void EnemyDestroy(EnemyBase _enemy)
    {
        _enemy.EnemyObjectDestroy();
    }
}
