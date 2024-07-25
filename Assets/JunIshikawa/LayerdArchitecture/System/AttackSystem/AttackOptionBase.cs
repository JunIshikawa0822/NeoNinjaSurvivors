using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackOptionBase
{
    protected GameStatus gameStat;
    protected bool attackBool;

    protected float attackTimer;

    public void GameStatusInit(GameStatus _gameStat)
    {
        this.gameStat = _gameStat;
    }

    public void AttackOptionSetUp()
    {
        attackBool = false;
        attackTimer = 0;
    }

    protected Vector3 AutoAttackVector(Transform _playerTrans, List<EnemyBase> _enemyList, float _maxDistance)
    {
        List<EnemyBase> enemiesWithinRangeList = new List<EnemyBase>();
        float r = Random.Range(-90, 90);
        //Debug.Log(r);
        Vector3 vec = _playerTrans.position + _playerTrans.up;//s,t(vec.x, vec.z)
        Vector3 poi = _playerTrans.position;//a,b(poi.x, poi.z)
        float x = Mathf.Cos(r * Mathf.Deg2Rad) * (vec.x - poi.x) - Mathf.Sin(r * Mathf.Deg2Rad) * (vec.z - poi.z);
        float z = Mathf.Sin(r * Mathf.Deg2Rad) * (vec.x - poi.x) + Mathf.Cos(r * Mathf.Deg2Rad) * (vec.z - poi.z);
        Vector3 rangeVec = new Vector3(-x, 0, z);

        //Debug.DrawLine(_playerTrans.position, rangeVec, Color.red, 5);

        foreach (EnemyBase enemy in _enemyList)
        {
            float dis = Vector3.Distance(enemy.transform.position, _playerTrans.position);
            if (dis <= _maxDistance)
            {
                if (Physics.Raycast(_playerTrans.position, (enemy.transform.position - _playerTrans.position).normalized, out RaycastHit hitInfo, dis))
                {
                    if (hitInfo.collider.gameObject == enemy.gameObject)
                    {
                        enemiesWithinRangeList.Add(enemy);
                        //Debug.Log("Enemyの場所" + (enemy.transform.position - _playerTrans.position));
                    }
                }
            }
        }

        if (enemiesWithinRangeList.Count > 0)
        {
            // 距離でソート
            enemiesWithinRangeList.Sort((a, b) => Vector3.Distance(a.transform.position, _playerTrans.position).CompareTo(Vector3.Distance(b.transform.position, _playerTrans.position)));
            rangeVec = enemiesWithinRangeList[0].transform.position - _playerTrans.position/*enemiesWithinRange.GetRange(0, Mathf.Min(3, enemiesWithinRange.Count))*/;
            Debug.Log("見えてない！");
        }

        //Debug.DrawLine(_playerTrans.position, rangeVec, Color.red, 2f);
        return rangeVec;
    }

    protected void AttackTimer(float _time)
    {
        if(this.attackTimer > _time)
        {
            attackTimer = 0;
            attackBool = true;
            Debug.Log("はいTrue");
        }

        attackTimer += Time.deltaTime;
    }
}
