using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReflectBulletSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate()
    {
        if (gameStat.bulletList.Count > 0)
        {
            for (int i = gameStat.bulletList.Count - 1; i >= 0; i--)
            {
                gameStat.bulletList[i].OnUpdate();
            }
        }

        if (gameStat.isAttackInput == true)
        {
            SimulReflectBulletInstantiate(
                gameStat.reflectBullet,
                gameStat.player.transform.position,
                gameStat.playerMouseVector,
                gameStat.reflectBulletObjectData.bulletSpeed,
                gameStat.reflectBulletObjectData.maxDistance,
                gameStat.reflectBulletObjectData.bulletDamage,
                gameStat.reflectBulletObjectData.penetrateCount,
                gameStat.reflectBulletList,
                gameStat.reflectBulletObjectData.simulNumLevel,
                gameStat.reflectBulletObjectData.bulletAngleLevelArray[gameStat.reflectBulletObjectData.angleLevel]);
        }
    }

    //弾丸同時生成
    private void SimulReflectBulletInstantiate(ReflectBullet _bullet, Vector3 _playerPos, Vector3 _attackVector, float _bulletSpeed, float _maxDistance, int _bulletDamage, int _penetrateCount, List<ReflectBullet> _bulletList, int _simulNum, int _angleLevel)
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
            //ReflectBulletInstantiate(_bullet, _playerPos, vec, _bulletSpeed, _maxDistance, _bulletDamage, _penetrateCount, _bulletList);
        }

    }

    //弾丸単体生成
    private void ReflectBulletInstantiate(ReflectBullet _bullet, Vector3 _playerPos, Vector3 _attackVector, float _bulletSpeed, float _maxDistance, int _bulletDamage, List<ReflectBullet> _bulletList)
    {
        Debug.Log("発射！！！");
        //弾丸を生成
        ReflectBullet bulletInstance = GameObject.Instantiate(_bullet, _playerPos, Quaternion.identity);
        bulletInstance.Init(_attackVector, _bulletSpeed, _maxDistance, _bulletDamage);
        //Actionに弾丸をListから削除する関数を登録
        bulletInstance.reflectBulletRemoveEvent += BulletRemove;
        //反射の処理
        bulletInstance.reflectEvent += ReflectFunction;
        //Actionに被弾時に実行する処理を登録
        bulletInstance.reflectBulletCollideEvent += BulletCollide;
        //弾丸のリストに追加
        _bulletList.Add(bulletInstance);
    }

    //弾丸をリストから削除
    private void BulletRemove(ReflectBullet _bullet)
    {
        //弾丸にアタッチされた自爆処理
        gameStat.reflectBulletList.Remove(_bullet);
    }

    //弾丸が敵に当たった時に実行（エフェクトetc...）
    private void BulletCollide(Collision _collision, ReflectBullet _reflectBullet)
    {
        if (_collision.gameObject.CompareTag("Wall"))
        {
            //_reflectBullet.Reflect();
        }
        else if (_collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵だよ");
            EnemyBase enemy = _collision.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;
            //敵のダメージ関数を起動
            enemy.EntityGetDamage(_reflectBullet.BulletDamage());
        }

    }

    //反射を行わせる
    private void ReflectFunction(ReflectBullet _reflectBullet, Collision _collision)
    {
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector3 reflectedVector = _reflectBullet.GetSetVector;

        if (_collision == null)
        {
            if (_reflectBullet.transform.position.x < leftBottom.x)
            {
                reflectedVector = Vector3.Reflect(_reflectBullet.GetSetVector, Vector3.right);
            }
            else if (_reflectBullet.transform.position.x >= rightTop.x)
            {
                reflectedVector = Vector3.Reflect(_reflectBullet.GetSetVector, -Vector3.right);
            }
            else if (_reflectBullet.transform.position.y < leftBottom.y)
            {
                reflectedVector = Vector3.Reflect(_reflectBullet.GetSetVector, Vector3.forward);
            }
            else if (_reflectBullet.transform.position.y >= rightTop.y)
            {
                reflectedVector = Vector3.Reflect(_reflectBullet.GetSetVector, -Vector3.forward);
            }
        }
        else
        {
            //reflectedVector = Vector3.Reflect(_reflectBullet.GetSetVector, _collision.gameObject.);
        }

        _reflectBullet.GetSetVector = reflectedVector;
    }
}
