using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBulletSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate(){
        if (gameStat.autoBulletList.Count > 0)
        {
            for (int i = gameStat.autoBulletList.Count - 1; i >= 0; i--)
            {
                gameStat.autoBulletList[i].OnUpdate();
            }
        }

        
            Vector3? autoLaunchPos = LaunchDetection(gameStat.player.transform.position, gameStat.enemyList, gameStat.autoBulletRange);
            if(autoLaunchPos == null) return;

            // autoLaunchPos は null ではないことが保証されているため、null 許容型から値を取得
            Vector3 targetPos = autoLaunchPos.Value;

            float dis = Vector3.Distance(targetPos, gameStat.player.transform.position);
            if(dis < gameStat.autoBulletRange) {
                // Debug.Log(targetPos);
                SimulAutoBulletInstantiate(
                    gameStat.autoBullet,
                    gameStat.player.transform.position,
                    targetPos,
                    gameStat.autoBulletObjectData.bulletSpeed,
                    gameStat.autoBulletObjectData.maxDistance,
                    gameStat.autoBulletObjectData.bulletDamage,
                    gameStat.autoBulletObjectData.penetrateCount,
                    gameStat.autoBulletList,
                    gameStat.autoBulletObjectData.simulNumLevel,
                    gameStat.autoBulletObjectData.bulletAngleLevelArray[gameStat.autoBulletObjectData.angleLevel]
                );
            }

    }

    private void SimulAutoBulletInstantiate(AutoBullet _bullet, Vector3 _playerPos, Vector3 _enemyPos, float _bulletSpeed, float _maxDistance, int _bulletDamage, int _penetrateCount, List<AutoBullet> _autoBulletList, int _simulNum, int _angleLevel) {
        Vector3 attackVector = (_enemyPos - _playerPos).normalized;
        
        float theta;
        
        for (int i = 0; i < _simulNum; i++)
        {
            if (_simulNum % 2 == 1)
            {
                theta = Mathf.Pow(-1, i) * ((i + 1) / 2) * _angleLevel;
            }
            // 偶数なら
            else
            {
                theta = Mathf.Pow(-1, i) * ((i + 1) / 2) * _angleLevel + _angleLevel / 2;
            }

            Vector3 vec = Quaternion.Euler(0, theta, 0) * attackVector;

            AutoBulletInstantiate(_bullet, _playerPos, vec, _bulletSpeed, _maxDistance, _bulletDamage, _penetrateCount, _autoBulletList);
        }
    }

    private void AutoBulletInstantiate(AutoBullet _bullet, Vector3 _playerPos, Vector3 _attackVector, float _bulletSpeed, float _maxDistance, int _bulletDamage, int _penetrateCount, List<AutoBullet> _bulletList){
        //弾丸を生成
        AutoBullet bulletInstance = GameObject.Instantiate(_bullet, _playerPos, Quaternion.identity);
        bulletInstance.Init(_attackVector, _bulletSpeed, _maxDistance, _bulletDamage, _penetrateCount);
        //Actionに弾丸をListから削除する関数を登録
        bulletInstance.AutoBulletRemoveEvent += BulletRemove;
        //Actionに被弾時に実行する処理を登録
        bulletInstance.AutoBulletCollideEvent += BulletCollide;
        //弾丸のリストに追加
        _bulletList.Add(bulletInstance);
    }

    private Vector3? LaunchDetection(Vector3 _playerPos, List<EnemyBase> _enemyList, float _maxDistance) {
    // 最小距離の初期値
    float nearestDistance = float.MaxValue;
    Vector3? launchDestinationPos = null;

    foreach(var enemy in _enemyList) {
        // エネミーとの距離を計測
        float dis = Vector3.Distance(enemy.transform.position, _playerPos);

        // エネミーが最大距離以内かチェック
        if(dis <= _maxDistance) {
            // レイキャストで障害物の有無をチェック
            if(Physics.Raycast(_playerPos, (enemy.transform.position - _playerPos).normalized, out RaycastHit hitInfo, dis)) {
                // Raycastで衝突したオブジェクトが自分自身でないことを確認
                if(hitInfo.collider.gameObject != enemy.gameObject) {
                    continue;
                }
            }
            // 最小距離の更新
            if(dis < nearestDistance) {
                nearestDistance = dis;
                launchDestinationPos = enemy.transform.position;
            }
        }
    }

    return launchDestinationPos;
}




    //弾丸をリストから削除
    private void BulletRemove(AutoBullet _bullet)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.autoBulletList.Remove(_bullet);
    }

    //弾丸が敵に当たった時に実行（エフェクトetc...）
    private void BulletCollide(Collision _collision, AutoBullet _reflectBullet)
    {
        if (_collision.gameObject.CompareTag("Wall"))
        {
            //_reflectBullet.Reflect();
        }
        else if (_collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("敵だよ");
            EnemyBase enemy = _collision.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;
            //敵のダメージ関数を起動
            enemy.EntityGetDamage(_reflectBullet.BulletDamage());
        }

    }
}
