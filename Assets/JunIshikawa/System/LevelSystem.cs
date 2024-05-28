using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : SystemBase, IOnLateUpdate
{
    
    public void OnLateUpdate()
    {
        //playerのリアルタイムのレベル
        gameStat.playerLevel = ConvertExpToLevel(gameStat.playerTotalExp, gameStat.playerExpRatio, gameStat.playerPrimeDemandExp);

        //次のレベルアップまでに必要な経験値を計算
        gameStat.barMaxValue = DemandExpForLevelUp(gameStat.playerLevel, gameStat.playerExpRatio);

        //現在までの経験値の累積から今のレベルまでに必要な経験値の累積を引くことで、今のレベルに達してから獲得した経験値の量を計算
        gameStat.accumeExpUntilNowLevel = AccumeExpToSpecificLevel(gameStat.playerLevel, gameStat.playerPrimeDemandExp, gameStat.playerExpRatio);
        gameStat.barProgressValue = BarProgress(gameStat.playerTotalExp, gameStat.accumeExpUntilNowLevel);

        //レベルアップしているか確認
        gameStat.isLevelUp = LevelUpCheck(gameStat.playerPreLevel, gameStat.playerLevel);

        //レベルアップした時の処理
        if (!gameStat.isLevelUp) return;
        gameStat.playerPreLevel = gameStat.playerLevel;
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

    private bool LevelUpCheck(int _preLevel, int _playerLevel)
    {
        bool isLevelUp = false;
        
        if(_preLevel < _playerLevel)
        {
            isLevelUp = true;
        }
        else
        {
            isLevelUp = false;
        }

        return isLevelUp;
    }
}
