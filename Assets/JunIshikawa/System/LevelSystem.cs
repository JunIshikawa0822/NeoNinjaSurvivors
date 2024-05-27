using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : SystemBase, IOnLateUpdate
{
    public void OnLateUpdate()
    {
        gameStat.playerLevel = ConvertExpToLevel(gameStat.playerTotalExp, gameStat.playerExpRatio, gameStat.playerPrimeDemandExp);
        gameStat.barMaxValue = DemandExpForLevelUp(gameStat.playerLevel, gameStat.playerExpRatio);
        gameStat.accumeExpUntilNowLevel = AccumeExpToSpecificLevel(gameStat.playerLevel, gameStat.playerPrimeDemandExp, gameStat.playerExpRatio);

        gameStat.barProgressValue = BarProgress(gameStat.playerTotalExp, gameStat.accumeExpUntilNowLevel);
    }

    //経験値からレベルを計算
    private int ConvertExpToLevel(int _playerTotalExp, float _expRatio, int _primeDemandExp)
    {
        //レベル1から2に必要な経験値（初項） = primeExp
        //公比 = ratio
        int level = (int)Mathf.Log(((_playerTotalExp * _expRatio - _playerTotalExp) / _primeDemandExp) + 1, _expRatio);

        return level + 1;
    }

    //レベルから次のレベルアップに必要な経験値を計算
    private int DemandExpForLevelUp(int _playerLevel, float _expRatio)
    {
        int demandEXP = Mathf.FloorToInt(_expRatio * Mathf.Pow(_expRatio, _playerLevel - 1));

        return demandEXP;
    }

    //指定したレベルまでの累積経験値計算
    private int AccumeExpToSpecificLevel(int _playerLevel, int _primeDemandExp, float _expRatio)
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

    //現在の進行度
    private int BarProgress(int _playerTotalExp, int _accumeExp)
    {
        int expToNextLevel = _playerTotalExp - _accumeExp;

        return expToNextLevel;
    }
}
