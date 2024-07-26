using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSystem : SystemBase, IOnUpdate
{
    public void OnUpdate()
    {
        if(gameStat.player.GetEntityHp < 1)
        {
            AllFunctionOff();
            AllEnemyOff();
        }
    }

    private void AllFunctionOff()
    {
        gameStat.isAuraUsing = false;
        gameStat.isBulletUsing = false;
        gameStat.isFunnelUsing = false;
        gameStat.isShurikenUsing = false;
        gameStat.isThunderUsing = false;
    }

    private void AllEnemyOff()
    {
        for (int i = gameStat.enemyList.Count - 1; i >= 0; i--)
        {
            gameStat.enemyList[i].NavMeshAgentIsStopped(true);
        }
    }
}
