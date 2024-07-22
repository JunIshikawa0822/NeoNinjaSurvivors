using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOptionBase
{
    protected GameStatus gameStat;
    protected Vector3 autoAttackVector;

    public void GameStatusInit(GameStatus _gameStat)
    {
        this.gameStat = _gameStat;
    }

    public virtual void AttackOptionSetUp()
    {
        //this.timer = 0;
        this.autoAttackVector = Vector3.zero;
    }

    protected Vector3 AutoAttackVector(Transform _playerTrans, List<EnemyBase> _enemyList, float _maxDistance)
    {
        List<EnemyBase> enemiesWithinRangeList = new List<EnemyBase>();
        float r = Random.Range(-90, 90);
        Debug.Log(r);

        Vector3 rangeVec = _playerTrans.position;

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
                        Debug.Log("Enemyの場所" + (enemy.transform.position - _playerTrans.position));
                    }
                }
            }
        }

        if (enemiesWithinRangeList.Count > 0)
        {
            // 距離でソート
            enemiesWithinRangeList.Sort((a, b) => Vector3.Distance(a.transform.position, _playerTrans.position).CompareTo(Vector3.Distance(b.transform.position, _playerTrans.position)));
            rangeVec = enemiesWithinRangeList[0].transform.position - _playerTrans.position/*enemiesWithinRange.GetRange(0, Mathf.Min(3, enemiesWithinRange.Count))*/;
        }

        //Debug.DrawLine(_playerTrans.position, rangeVec, Color.red, 2f);
        return rangeVec;
    }
}
