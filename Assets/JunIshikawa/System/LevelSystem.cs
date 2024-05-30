using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : SystemBase, IOnLateUpdate
{
    
    public void OnLateUpdate()
    {
        //playerのリアルタイムのレベル
        //gameStat.playerLevel = ConvertExpToLevel(gameStat.playerTotalExp, gameStat.playerPrimeDemandExp, gameStat.playerExpRatio);

        //次のレベルアップまでに必要な経験値を計算
        gameStat.playerExpSliderMaxValue = DemandExpForLevelUp(gameStat.playerLevel, gameStat.playerPrimeDemandExp, gameStat.playerExpRatio);

        //現在までの経験値の累積から今のレベルまでに必要な経験値の累積を引くことで、今のレベルに達してから獲得した経験値の量を計算
        gameStat.accumeExpUntilNowLevel = AccumeExpToSpecificLevel(gameStat.playerLevel, gameStat.playerPrimeDemandExp, gameStat.playerExpRatio);
        gameStat.playerExpSliderProgressValue = PlayerExpProgress(gameStat.playerTotalExp, gameStat.accumeExpUntilNowLevel);

        //レベルアップしているか確認
        gameStat.isLevelUp = LevelUpCheck(gameStat.playerExpSliderProgressValue, gameStat.playerExpSliderMaxValue);

        //レベルアップした時の処理
        if (!gameStat.isLevelUp) return;
        gameStat.playerLevel++;
    }

    //経験値からレベルを計算
    private int ConvertExpToLevel(int _playerTotalExp, int _primeDemandExp, float _expRatio)
    {
        //レベル1から2に必要な経験値（初項） = primeExp
        //公比 = ratio
        int level = (int)Mathf.Log(((_playerTotalExp * _expRatio - _playerTotalExp) / _primeDemandExp) + 1, _expRatio);

        return level;
    }

    //レベルから次のレベルアップに必要な経験値を計算
    private int DemandExpForLevelUp(int _playerLevel, int _primeDemandExp, float _expRatio)
    {
        int demandEXP = Mathf.FloorToInt(_primeDemandExp * Mathf.Pow(_expRatio, _playerLevel - 1));
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
    private int PlayerExpProgress(int _playerTotalExp, int _accumeExp)
    {
        int expToNextLevel = _playerTotalExp - _accumeExp;

        return expToNextLevel;
    }

    private bool LevelUpCheck(int _sliderProgressValue, int _sliderMaxValue)
    {
        bool isLevelUp = false;
        
        if(_sliderMaxValue > _sliderProgressValue)
        {
            isLevelUp = false;
        }
        else
        {
            isLevelUp = true;
        }

        return isLevelUp;
    }
}
