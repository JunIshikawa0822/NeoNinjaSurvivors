using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : SystemBase, IOnUpdate
{
    public override void SetUp()
    {
        base.SetUp();

        gameStat.player.EntityComponentSetUp();
        gameStat.player.EntityHpSetUp(gameStat.playerDataList[0].playerMaxHp);
        gameStat.player.Init(false);
    }

    public void OnUpdate()
    {
        if (gameStat.isMoveInput)
        {
            PlayerMove(gameStat.player, gameStat.playerMouseVector, gameStat.playerMoveMaxDistance, gameStat.playerMoveRayHitLayer);
        }

        //PlayerAnimation(gameStat.player);
    }

    private void PlayerMove(Player _player, Vector3 _mouseVec, float _maxRayDistance, int _rayHitLayerMask)
    {
        //Debug.DrawRay(originPos, directionVec, Color.red, 3);
        if (!Physics.Raycast(_player.transform.position, _mouseVec, out RaycastHit _hitInfo, _maxRayDistance, _rayHitLayerMask))
        {
            //できない
            return;
        }
        else
        {
            //できる
            MoveAndRot(_player, _hitInfo);
        }

        void MoveAndRot(Player _player, RaycastHit hitInfo)
        {
            _player.transform.position = hitInfo.point + hitInfo.normal;
            _player.transform.rotation = Quaternion.LookRotation(-Vector3.up, hitInfo.normal);
        }
    }

    private void PlayerAnimation(Player _player, bool _moveWaitAnimParam, bool _attackAnimParam)
    {
        Debug.Log("aaa");
        _player.ParameterSet(_attackAnimParam);
    }
}
