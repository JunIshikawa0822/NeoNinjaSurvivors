using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate()
    {
        if(gameStat.funnelList.Count > 0)
        {
            for (int i = gameStat.funnelList.Count - 1; i >= 0; i--)
            {
                gameStat.funnelList[i].OnUpdate();
            }
        }

        if (gameStat.isFunnelUsing)
        {
            if (this.attackBool)
            {
                SimulFunnelInstantiate(
                    gameStat.funnelObjectData.simulNum,
                    gameStat.funnel,
                    gameStat.player.transform,
                    gameStat.funnelObjectData.radius,
                    gameStat.funnelObjectData.theta,
                    gameStat.funnelObjectData.moveMaxDistance,
                    gameStat.funnelObjectData.speed,
                    gameStat.funnelObjectData.damage,
                    gameStat.funnelList
                    );

                attackBool = false;
            }
        }

        AttackTimer(gameStat.funnelObjectData.coolTime);
    }

    private void SimulFunnelInstantiate(int _simulNum, Funnel _funnel, Transform _playerTrans, float _radius, float _theta, float _moveMaxDistance, float _speed, int _damage, List<Funnel> _funnelList)
    {
        for (int i = 0; i < _simulNum; i++)
        {
            FunnelInstantiate(_funnel, _playerTrans, _radius, _theta, _moveMaxDistance, _speed, _damage, _funnelList);
        } 
    }

    private void FunnelInstantiate(Funnel _funnel, Transform _playerTrans, float _radius, float _theta, float _moveMaxDistance, float _speed, int _damage, List<Funnel> _funnelList)
    {
        Funnel funnel = GameObject.Instantiate<Funnel>(
            _funnel,
            _playerTrans.position,
            Quaternion.identity
            );

        funnel.Init(_playerTrans, _radius, _theta, _moveMaxDistance, _speed, _damage);
        funnel.funnelRemoveEvent += (funnel) => gameStat.funnelList.Remove(funnel);
        funnel.onFunnelUpdate += FunnelThetaSet;
        funnel.funnelCollideEvent += FunnelCollide;

        _funnelList.Add(funnel);
    }

    private void FunnelThetaSet()
    {
        if (gameStat.funnelList.Count != gameStat.funnelListCount)
        {
            if (gameStat.funnelList.Count > 1)
            {
                foreach (Funnel funnel in gameStat.funnelList)
                {
                    float theta = (360 / gameStat.funnelList.Count) * (gameStat.funnelList.IndexOf(funnel));
                    funnel.SetTheta(gameStat.funnelList[0].GetTheta() + theta * Mathf.Deg2Rad);
                }
            }
            gameStat.funnelListCount = gameStat.funnelList.Count;
        }
    }

    private void FunnelCollide(Collider _collider, Funnel _funnel)
    {
        if (_collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵だよ");
            EnemyBase enemy = _collider.gameObject.GetComponent<EnemyBase>();
            if (enemy == null) return;

            Debug.Log("当たった");
            //敵のダメージ関数を起動
            enemy.EntityGetDamage(_funnel.FunnelDamage());
            //_funnel.OnTriggerNextAction();//リストから削除
            //_funnel.FunnelDestroy();//オブジェクトを破壊
        }
    }
}
