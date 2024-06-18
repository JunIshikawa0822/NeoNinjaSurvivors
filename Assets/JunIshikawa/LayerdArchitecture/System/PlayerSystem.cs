using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSystem : SystemBase, IOnUpdate
{
    public override void SetUp()
    {
        base.SetUp();

        gameStat.player.EntityComponentSetUp();
        gameStat.player.Init(false);
    }

    public void OnUpdate()
    {
        if (gameStat.isMoveInputUp)
        {
            //ワープ
            PlayerWarp(gameStat.player);
            gameStat.player.PlayerWarpRag(gameStat.player, gameStat.playerMouseVector, gameStat.playerMoveMaxDistance, gameStat.playerMoveRayHitLayer);
            PlayerMoveWait(gameStat.player, false);
        }

        if (gameStat.isMoveInput)
        {
            //ワープ待機
            PlayerMoveWait(gameStat.player, true);
        }

        if (gameStat.isAttackInput)
        {
            //攻撃状態
            PlayerAttack(gameStat.player, true);
        }
        else
        {
            //攻撃状態解除
            PlayerAttack(gameStat.player, false);
        }

        //PlayerAnimation(gameStat.player);
    }

    

    private void PlayerAttack(Player _player, bool _attackBool)
    {
        _player.attackSetBool(_attackBool);
    }

    private void PlayerMoveWait(Player _player, bool _moveWaitBool)
    {
        _player.moveWaitSetBool(_moveWaitBool);
    }

    private void PlayerWarp(Player _player)
    {
        _player.warpSetTrigger();
    }
}
