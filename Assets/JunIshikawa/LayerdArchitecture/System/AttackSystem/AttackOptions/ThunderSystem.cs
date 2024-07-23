using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate(){
        if(gameStat.isThunderAttack)
        {
            gameStat.thunderObjectData.thunderTime += Time.deltaTime;
            if(gameStat.thunderObjectData.thunderTime > gameStat.thunderObjectData.thunderInterval)
            {
                //ここで一定範囲内に落雷
                ThunderboltAttack(gameStat.thunder, gameStat.player.transform.position, gameStat.thunderObjectData.thunderRange, gameStat.thunderObjectData.thunderDamage);
                gameStat.thunderObjectData.thunderTime = 0f; 
            }
        }
    }

    private void ThunderboltAttack(Thunder _thunder, Vector3 _playerPos, float _thunderRange, int _thunderDamage)
    {
        // ランダムな角度を生成
        float angle = Random.Range(0f, Mathf.PI * 2);
        
        // ランダムな距離を生成
        float distance = Random.Range(0f, _thunderRange);
        
        // 楕円座標系を使って新しい位置を計算
        Vector3 thunderPos = new Vector3(
            _playerPos.x + Mathf.Cos(angle) * distance,
            _playerPos.y,
            _playerPos.z + Mathf.Sin(angle) * distance
        );

        // Thunderのインスタンスを生成
        Thunder thunderInstance = GameObject.Instantiate(_thunder, thunderPos, Quaternion.identity);
        thunderInstance.Init(_thunderDamage);
        thunderInstance.thunderTriggerEvent += ThunderTrigger;
    }

    private void ThunderTrigger(Collider _other, Thunder _thunder){
        if (_other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("雷が当たった！！！");
            EnemyBase enemy = _other.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;

            //敵のダメージ関数を起動
            enemy.EnemyGetDamage(_thunder.ThunderDamage(), gameStat.player.transform.position, enemy.transform.position, gameStat.thunderObjectData.thunderNockBackStrength);
        }
    }
}
