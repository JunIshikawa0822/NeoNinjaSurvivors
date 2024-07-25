using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : SystemBase, IOnUpdate
{

    public void OnUpdate()
    {
        if (gameStat.enemyList.Count > 0)
        {
            //エネミーのNavMesh挙動
            for (int i = gameStat.enemyList.Count - 1; i >= 0; i--)
            {
                if (!gameStat.isLevelUp)
                {
                    if(gameStat.enemyList[i] == null) return;
                    //動かす
                    gameStat.enemyList[i].NavMeshAgentIsStopped(false);
                    gameStat.enemyList[i].NavMeshDestinationSet(gameStat.player.transform.position);
                    gameStat.enemyList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameStat.enemyList[i].OnUpdate();
                    //プレイヤーを向く
                    if (gameStat.enemyList[i].transform.position.x < gameStat.player.transform.position.x)
                    {
                        gameStat.enemyList[i].transform.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    }else{
                        gameStat.enemyList[i].transform.GetComponentInChildren<SpriteRenderer>().flipX = false;
                    }
                }
                else
                {
                    gameStat.enemyList[i].NavMeshAgentIsStopped(true);
                }
            }
        }

        if(gameStat.isLevelUp) return;
        
        gameStat.elapsedTime += Time.deltaTime;
        //エネミー生成工場（１秒ごとに生成確率に応じて生成される）無限増殖怖いので50体に制限
        if(gameStat.elapsedTime > gameStat.spawnInterval && gameStat.enemyList.Count < gameStat.spawnLimitNum){
            float randomParam = Random.Range(0,100f);
            if(randomParam < gameStat.spawnProbabilities[5]){
                GoldEnemyInstantiate(gameStat.goldEnemy, gameStat.enemyDataList[5], gameStat.enemyList, gameStat.player.transform.position, 1, gameStat.spawnRadius);
            }
            else if(randomParam < gameStat.spawnProbabilities[4]){
                FlyingEnemyInstantiate(gameStat.flyingEnemy, gameStat.enemyDataList[4], gameStat.enemyList, gameStat.player.transform.position, 1, gameStat.spawnRadius);
            }
            else if(randomParam < gameStat.spawnProbabilities[3]){
                BlackEnemyInstantiate(gameStat.blackEnemy, gameStat.enemyDataList[3], gameStat.enemyList, gameStat.player.transform.position, 1, gameStat.spawnRadius);
            }
            else if(randomParam < gameStat.spawnProbabilities[2]){
                ArmEnemyInstantiate(gameStat.armEnemy, gameStat.enemyDataList[2], gameStat.enemyList, gameStat.player.transform.position, 1, gameStat.spawnRadius);
            }
            else if(randomParam < gameStat.spawnProbabilities[1]){
                GreenEnemyInstantiate(gameStat.greenEnemy, gameStat.enemyDataList[1], gameStat.enemyList, gameStat.player.transform.position, 5, gameStat.spawnRadius);
            }
            else if(randomParam < gameStat.spawnProbabilities[0]){
                EyeballEnemyInstantiate(gameStat.eyeballEnemy, gameStat.enemyDataList[0], gameStat.enemyList, gameStat.player.transform.position, 1, gameStat.spawnRadius);
            }

            gameStat.elapsedTime = 0f;
            //Debug.Log("１秒経過");
        }
    }

    private int PlayerPower(int _elapsedTime) {
        int playerPower = _elapsedTime;
        Debug.Log("プレイヤーの能力値："+ playerPower);
        return playerPower;
    }
　　//以下　敵生成関数

    private void EyeballEnemyInstantiate(EyeballEnemy _eyeballEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        int leaderPos = Random.Range(0,_enemyNum - 1);
        for(int i = 0; i < _enemyNum ; i ++)
        {
            float angle;
            if(_enemyNum > 1) angle = 360/_enemyNum * leaderPos;
            else  angle = Random.Range(1,360);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.0f, z);

            EnemyBase enemy = GameObject.Instantiate<EyeballEnemy>(_eyeballEnemy, enemyPos, Quaternion.identity);
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
        
    }
    private void ArmEnemyInstantiate(ArmEnemy _armEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        int leaderPos = Random.Range(0,_enemyNum - 1);
        for(int i = 0; i < _enemyNum ; i ++)
        {
            float angle;
            if(_enemyNum > 1) angle = 360/_enemyNum * leaderPos;
            else  angle = 360/_enemyNum * leaderPos;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.0f, z);

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
        
    }
    private void GreenEnemyInstantiate(GreenEnemy _greenEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        int leaderPos = Random.Range(0,_enemyNum - 1);
        for(int i = 0; i < _enemyNum ; i ++)
        {
            float angle;
            if(_enemyNum > 1) angle = 360/_enemyNum * leaderPos;
            else  angle = 360/_enemyNum * leaderPos;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.0f, z);

            EnemyBase enemy = GameObject.Instantiate<GreenEnemy>(_greenEnemy, enemyPos, Quaternion.identity);
            //enemy.GetComponent<CapsuleCollider>().isTrigger = false;
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
        
    }
    private void BlackEnemyInstantiate(BlackEnemy _blackEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        for(int i = 0; i < _enemyNum ; i ++)
        {
            int angle = Random.Range(1,360);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.6f, z);//微調整用の0.6f

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
        
    }
    private void FlyingEnemyInstantiate(FlyingEnemy _flyingEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        for(int i = 0; i < _enemyNum ; i ++)
        {
            int angle = Random.Range(1,360);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.6f, z);//微調整用の0.6f

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
        
    }
    private void GoldEnemyInstantiate(GoldEnemy _goldEnemy, EnemyData _data, List<EnemyBase> _enemyList, Vector3 _playerPos, int _enemyNum,float _spawnRadius)
    {
        Vector3 center = _playerPos;
        for(int i = 0; i < _enemyNum ; i ++)
        {
            int angle = Random.Range(1,360);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _spawnRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * _spawnRadius;

            Vector3 enemyPos = center + new Vector3(x, 0.6f, z);//微調整用の0.6f

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
        
    }
    
    
    //敵が何かに当たった時に発動
    private void EnemyCollide(Collision _collision, EnemyBase _enemy)
    {
        if(!_collision.transform.CompareTag("Player"))return;

        Player player = _collision.transform.GetComponent<Player>();
        if (player == null) return;

        //プレイヤーのダメージ関数を起動
        player.PlayerGetDamage(_enemy.GetEnemyAttack);

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
