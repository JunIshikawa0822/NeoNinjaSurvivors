using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : SystemBase, IOnLateUpdate
{
    [System.NonSerialized]
    public int totalPlayerEXP = 0;

    //[System.NonSerialized]
    public int playerLevel = 0;

    //レベル1から2に必要な経験値（初項）
    [Range(3, 10), System.NonSerialized]
    int demandEXPprime = 3;

    //レベルが上がるにつれて、レベルアップまでに必要な経験値を増やすための公比
    [Range(1, 2), System.NonSerialized]
    float EXPRatio = 1.5f;

    public void OnLateUpdate()
    {

    }

    //経験値からレベルを計算
    private int EXPtoLevel(int _playerTotalExp, float _expRatio, int _primeDemandExp)
    {
        //レベル1から2に必要な経験値（初項） = primeEXP
        //公比 = ratio
        int level = (int)Mathf.Log(((_playerTotalExp * _expRatio - _playerTotalExp) / _primeDemandExp) + 1, _expRatio);

        return level + 1;
    }

    //レベルから次のレベルアップに必要な経験値を計算
    private int demandEXPtoNextLevel(int _playerLevel, float _expRatio)
    {
        int demandEXP = Mathf.FloorToInt(_expRatio * Mathf.Pow(_expRatio, _playerLevel - 1));

        return demandEXP;
    }

    //指定したレベルまでの累積経験値計算
    private int AccumulationEXP(int _playerLevel, int _primeDemandExp, float _expRatio)
    {
        int accumeExp;

        if (_playerLevel == 1)
        {
            accumeExp = 0;
        }
        else
        {
            accumeExp = Mathf.FloorToInt(_primeDemandExp * ((Mathf.Pow(_expRatio, _playerLevel - 1) - 1) / (_expRatio - 1)));
        }

        return accumeExp;
    }

    //private float BarPersent(int nowLevel)
    //{
    //    int EXPtoNextLevel = totalPlayerEXP - (int)AccumulationEXP(nowLevel);

    //    //Debug.Log("nowEXP" + EXPtoNextLevel + ", dem：" + demandEXPtoNextLevel(nowLevel));

    //    float persent = Mathf.FloorToInt(((float)EXPtoNextLevel / (float)demandEXPtoNextLevel(nowLevel)) * 1000);

    //    //Debug.Log(persent);

    //    return persent;
    //}
}
