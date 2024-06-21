using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : SystemBase, IOnUpdate
{
    // public override void SetUp()
    // {
    //     spawnNormal(gameStat.player.transform.position, 1, gameStat.spawnRadius, gameStat.eyeballEnemy, gameStat.enemyDataList[0], gameStat.enemyList);
    // }

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
                    gameStat.enemyList[i].NavMeshAgentIsStopped(false);
                    gameStat.enemyList[i].NavMeshDestinationSet(gameStat.player.transform.position);
                    
                }
                else
                {
                    gameStat.enemyList[i].NavMeshAgentIsStopped(true);
                }
            }
        }
    }


    

    // private void spawnSwarm(Vector3 _playerPos,int _enemyNum, float _spawnRadius, string _enemyName)
    // {
    //     //SpawnRadius = 30.0f;
    //     Vector3 center = _playerPos;
    //     int leaderPos = Random.Range(0,_enemyNum - 1);
    //     for(int i = 0; i < _enemyNum ; i ++)
    //     {
            
    //         float angle = 360/_enemyNum * leaderPos;
    //         float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
    //         float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

    //         Vector3 enemyPos = center + new Vector3(x, 0, z);

            
    //     }
    // }

    private void spawnNormal(Vector3 _playerPos,int _enemyNum, float _spawnRadius, EnemyBase _allEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        //GameObject spawnEnemy;
        Vector3 center = _playerPos;
        for(int i = 0; i < _enemyNum ; i ++)
        {
            int angle = Random.Range(1,360);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.6f, z);//微調整用の0.6f

            EnemyInstantiate(_allEnemy, _data, _enemyList, enemyPos); 
        }
    }

　　//以下　敵生成関数
    private void EnemyInstantiate(EnemyBase _allEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _enemyPos)
    {
        EnemyBase enemy = GameObject.Instantiate<EnemyBase>(_allEnemy, _enemyPos, Quaternion.identity);
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

    private void ArmEnemyInstantiate(ArmEnemy _armEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<ArmEnemy>(_armEnemy, _data.instantiatePos, Quaternion.identity);

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

    private void BlackEnemyInstantiate(BlackEnemy _blackEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<BlackEnemy>(_blackEnemy, _data.instantiatePos, Quaternion.identity);

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
    
    private void FlyingEnemyInstantiate(FlyingEnemy _flyingEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<FlyingEnemy>(_flyingEnemy, _data.instantiatePos, Quaternion.identity);

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

    private void GoldEnemyInstantiate(GoldEnemy _goldEnemy, EnemyData _data, List<EnemyBase> _enemyList)
    {
        EnemyBase enemy = GameObject.Instantiate<GoldEnemy>(_goldEnemy, _data.instantiatePos, Quaternion.identity);

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
