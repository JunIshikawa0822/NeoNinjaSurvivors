using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSystem : AttackOptionBase, IOnUpdate
{
    public override void AttackOptionSetUp()
    {
        gameStat.shurikenObjectData.InitializeShurikenLevels();
        gameStat.shurikenObjectData.SetLevel(gameStat.bulletSkillLevel);
    }

    public void OnUpdate()
    {
        if (gameStat.shurikenList.Count > 0)
        {
            for (int i = gameStat.shurikenList.Count - 1; i >= 0; i--)
            {
                gameStat.shurikenList[i].OnUpdate();
            }
        }

        if (gameStat.isShurikenUsing == true)
        {
            if (this.attackBool)
            {
                SimulBulletInstantiate(
                    gameStat.shuriken,
                    gameStat.player.transform.position,
                    this.AutoAttackVector(gameStat.player.transform, gameStat.enemyList, gameStat.attackRange),
                    gameStat.shurikenObjectData.BulletSpeed,
                    gameStat.shurikenObjectData.MaxDistance,
                    gameStat.shurikenObjectData.BulletDamage,
                    gameStat.shurikenList,
                    gameStat.shurikenObjectData.SimulNumLevel,
                    gameStat.shurikenObjectData.shurikenAngleLevelArray[gameStat.shurikenObjectData.AngleLevel]);

                attackBool = false;

                Debug.Log("発射ァ!!!!");
            }
        }
        AttackTimer(gameStat.shurikenObjectData.CoolTime);
    }

    //弾丸同時生成
    private void SimulBulletInstantiate(Shuriken _shuriken, Vector3 _playerPos, Vector3 _attackVector, float _shurikenSpeed, float _maxDistance, int _shurikenDamage, List<Shuriken> _shurikenList, int _simulNum, int _angleLevel)
    {
        //角度計算
        float theta;

        for (int i = 0; i < _simulNum; i++)
        {
            //奇数なら
            if (_simulNum % 2 == 1)
            {
                theta = Mathf.Pow(-1, i) * ((i + 1) / 2) * _angleLevel;
            }
            //偶数なら
            else
            {
                theta = Mathf.Pow(-1, i) * ((i + 1) / 2) * _angleLevel + _angleLevel / 2;
            }
            Vector3 vec = Quaternion.Euler(0, theta, 0) * _attackVector;
            BulletInstantiate(_shuriken, _playerPos, vec, _shurikenSpeed, _maxDistance, _shurikenDamage, _shurikenList);
        }

    }

    //弾丸単体生成
    private void BulletInstantiate(Shuriken _bullet, Vector3 _playerPos, Vector3 _attackVector, float _shurikenSpeed, float _maxDistance, int _shurikenDamage, List<Shuriken> _shurikenList)
    {
        //弾丸を生成
        Shuriken shurikenInstance = GameObject.Instantiate(_bullet, _playerPos, Quaternion.identity);
        shurikenInstance.Init(_attackVector, _shurikenSpeed, _maxDistance, _shurikenDamage);
        //Actionに弾丸をListから削除する関数を登録
        shurikenInstance.shurikenRemoveEvent += ShurikenRemove;
        //Actionに被弾時に実行する処理を登録
        shurikenInstance.shurikenCollideEvent += ShurikenCollide;
        //弾丸のリストに追加
        _shurikenList.Add(shurikenInstance);
    }

    //弾丸をリストから削除
    private void ShurikenRemove(Shuriken _shuriken)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.shurikenList.Remove(_shuriken);
    }

    //弾丸が敵に当たった時に実行（エフェクトetc...）
    private void ShurikenCollide(Collider _collision, Shuriken _bullet)
    {
        if (_collision.gameObject.CompareTag("Wall"))
        {
            //壁にぶつかったときの破壊

            _bullet.OnTriggerNextAction();//リストから削除
            _bullet.ShurikenDestroy();//オブジェクトを破壊
        }
        else if (_collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵だよ");
            EnemyBase enemy = _collision.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;

            Debug.Log("当たった");
            //敵のダメージ関数を起動
            enemy.EntityGetDamage(_bullet.ShurikenDamage());
            
        }

    }
}
