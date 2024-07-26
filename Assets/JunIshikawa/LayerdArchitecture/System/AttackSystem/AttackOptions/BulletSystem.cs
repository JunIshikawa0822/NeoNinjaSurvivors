using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BulletSystem : AttackOptionBase, IOnUpdate
{
    public override void AttackOptionSetUp()
    {
        //gameStat.bulletObjectData.InitializeBulletLevels();
        gameStat.bulletObjectData.SetLevel(gameStat.bulletSkillLevel);
    }
    public void OnUpdate()
    {
        
        if (gameStat.bulletList.Count > 0)
        {
            for (int i = gameStat.bulletList.Count - 1; i >= 0; i--)
            {
                gameStat.bulletList[i].OnUpdate();
            }
        }

        
        if (gameStat.isBulletUsing == true)
        {
            if (this.attackBool)
            {
                SimulBulletInstantiate(
                    gameStat.bullet,
                    gameStat.player.transform.position,
                    this.AutoAttackVector(gameStat.player.transform, gameStat.enemyList, gameStat.attackRange),
                    gameStat.bulletObjectData.BulletSpeed,
                    gameStat.bulletObjectData.MaxDistance,
                    gameStat.bulletObjectData.BulletDamage,
                    gameStat.bulletObjectData.PenetrateCount,
                    gameStat.bulletList,
                    gameStat.bulletObjectData.SimulNumLevel,
                    gameStat.bulletObjectData.bulletAngleLevelArray[gameStat.bulletObjectData.AngleLevel]);

                attackBool = false;

                Debug.Log("発射ァ!!!!");
            }
        }

        //float r = 0;
        //Debug.Log(r);
        //Vector3 vec = gameStat.player.transform.position + gameStat.player.transform.up;//s,t(vec.x, vec.z)
        //Vector3 poi = gameStat.player.transform.position;//a,b(poi.x, poi.z)
        //float x = Mathf.Cos(r * Mathf.Deg2Rad) * (vec.x - poi.x) - Mathf.Sin(r * Mathf.Deg2Rad) * (vec.z - poi.z);
        //float z = Mathf.Sin(r * Mathf.Deg2Rad) * (vec.x - poi.x) + Mathf.Cos(r * Mathf.Deg2Rad) * (vec.z - poi.z);
        //Vector3 roundVec = new Vector3(-x, 0, z);

        ////Vector3 rangeVec = gameStat.player.transform.position + roundVec;
        //Debug.DrawLine(gameStat.player.transform.position, gameStat.player.transform.position + roundVec * 5, Color.blue, 5);

        AttackTimer(gameStat.coolTime);
    }

    //弾丸同時生成
    private void SimulBulletInstantiate(Bullet _bullet, Vector3 _playerPos, Vector3 _attackVector, float _bulletSpeed, float _maxDistance, int _bulletDamage, int _penetrateCount, List<Bullet> _bulletList, int _simulNum, int _angleLevel)
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
            BulletInstantiate(_bullet, _playerPos, vec, _bulletSpeed, _maxDistance, _bulletDamage, _penetrateCount, _bulletList);
        }

    }

    //弾丸単体生成
    private void BulletInstantiate(Bullet _bullet, Vector3 _playerPos, Vector3 _attackVector, float _bulletSpeed, float _maxDistance, int _bulletDamage, int _penetrateCount, List<Bullet> _bulletList)
    {

        //弾丸を生成
        Bullet bulletInstance = GameObject.Instantiate(_bullet, _playerPos, Quaternion.identity);
        bulletInstance.Init(_attackVector, _bulletSpeed, _maxDistance, _bulletDamage, _penetrateCount);
        //Actionに弾丸をListから削除する関数を登録
        bulletInstance.bulletRemoveEvent += BulletRemove;
        //Actionに被弾時に実行する処理を登録
        bulletInstance.bulletCollideEvent += BulletCollide;
        //弾丸のリストに追加
        _bulletList.Add(bulletInstance);
    }

    //弾丸をリストから削除
    private void BulletRemove(Bullet _bullet)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.bulletList.Remove(_bullet);
    }

    //弾丸が敵に当たった時に実行（エフェクトetc...）
    private void BulletCollide(Collision _collision, Bullet _bullet)
    {
        if (_collision.gameObject.CompareTag("Wall"))
        {
            //壁にぶつかったときの破壊

            _bullet.OnTriggerNextAction();//リストから削除
            _bullet.BulletDestroy();//オブジェクトを破壊
        }
        else if (_collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵だよ");
            EnemyBase enemy = _collision.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;

            Debug.Log("当たった");
            //敵のダメージ関数を起動
            enemy.EntityGetDamage(_bullet.BulletDamage());
            //弾丸の貫通可能回数を１減らす
            int p = _bullet.PenetrateCount - 1;
            _bullet.PenetrateCount = p;
            //これ以上貫通できる場合ここでreturn
            if (_bullet.PenetrateCount >= 1) return;
            _bullet.OnTriggerNextAction();//リストから削除
            _bullet.BulletDestroy();//オブジェクトを破壊
        }

    }
}
