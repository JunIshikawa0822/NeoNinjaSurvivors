using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBase
{
    protected GameStatus gameStat;

    public void GameStatusInit(GameStatus _gameStat)
    {
        this.gameStat = _gameStat;
    }

    public virtual void Setup()
    {

    }
}
