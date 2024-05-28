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
        gameStat.expSliderMaxValue = DemandExpForLevelUp(gameStat.playerLevel, gameStat.playerExpRatio, gameStat.playerPrimeDemandExp);

        //現在までの経験値の累積から今のレベルまでに必要な経験値の累積を引くことで、今のレベルに達してから獲得した経験値の量を計算
        gameStat.accumeExpUntilNowLevel = AccumeExpToSpecificLevel(gameStat.playerLevel, gameStat.playerExpRatio, gameStat.playerPrimeDemandExp);

        gameStat.expSliderProgressValue = BarProgress(gameStat.playerTotalExp, gameStat.accumeExpUntilNowLevel);

        //レベルアップしているか確認
        //gameStat.isLevelUp = LevelUpCheck(gameStat.expSliderProgressValue, gameStat.expSliderMaxValue);

        //レベルアップした時の処理
        //if (!gameStat.isLevelUp) return;
        //gameStat.playerPreLevel = gameStat.playerLevel;
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
    private int DemandExpForLevelUp(int _playerLevel, float _expRatio, int _primeDemandExp)
    {
        int demandEXP = Mathf.FloorToInt(_primeDemandExp * Mathf.Pow(_expRatio, _playerLevel - 1));

        return demandEXP;
    }

    //指定したレベルまでの累積経験値計算
    private int AccumeExpToSpecificLevel(int _Level, float _expRatio, int _primeDemandExp)
    {
        int accumeExp;

        if (_Level == 1)
        {
            accumeExp = 0;
        }
        else
        {
            accumeExp = Mathf.FloorToInt(_primeDemandExp * ((Mathf.Pow(_expRatio, _Level - 1) - 1) / (_expRatio - 1)));
        }

        return accumeExp;
    }

    //現在の進行度
    private int BarProgress(int _playerTotalExp, int _accumeExp)
    {
        int expToNextLevel = _playerTotalExp - _accumeExp;

        return expToNextLevel;
    }

    //未定
    private bool LevelUpCheck(int _barProgress, int _barMax)
    {
        bool isLevelUp = false;
        
        if(_barMax == _barProgress)
        {
            isLevelUp = true;
            Debug.Log("Level Up!!!!!!!!!");
        }
        else
        {
            isLevelUp = false;
        }

        return isLevelUp;
    }
}
