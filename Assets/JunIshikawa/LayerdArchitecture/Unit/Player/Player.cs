using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    //animation用ステータス
    //private bool normalWaitAnimParam;
    //ワープ
    private bool warpAnimParam;
    //攻撃
    private bool attackAnimParam;
    //ワープ待機
    private bool moveWaitAnimParam;
    //攻撃時のダメージ
    private bool damageAnimParam;
    //ワープ待機中のダメージ
    private bool warpDamageAnimParam;

    private bool isOnWarp = false;

    public void Init(bool _attackAnimParam)
    {
        ParameterSet(_attackAnimParam);
    }

    

    public void OnUpdate()
    {
        
    }
    
    //ワープ
    public void warpSetTrigger()
    {
        entityAnimator.SetTrigger("isWarp");
    }
    //ワープ待機
    public void moveWaitSetBool(bool _moveWaitBool)
    {
        entityAnimator.SetBool("isMoveWait", _moveWaitBool);
    }
    //攻撃
    public void attackSetBool(bool _attackBool)
    {
        entityAnimator.SetBool("isAttack", _attackBool);
    }
    //ダメージ
    private void damageSetTrigger()
    {
        entityAnimator.SetTrigger("isDamage");
    }

    public void ParameterSet(bool _attackAnimParam)
    {
        attackAnimParam = _attackAnimParam;
    }

    public bool IsOnWarpEnable()
    {
        return isOnWarp;
    }

    public void IsOnWarpControl()
    {
        isOnWarp = !isOnWarp;
    }

    // public void PlayerWarpRag(Player _player, Vector3 _mouseVec, float _maxRayDistance, int _rayHitLayerMask)
    // {
    //     StartCoroutine(WarpCoroutine(_player, _mouseVec, _maxRayDistance,_rayHitLayerMask));
    //     //Invoke(PlayerMove(_player, _mouseVec, _maxRayDistance, _rayHitLayerMask),0.4f);
    // }

    // IEnumerator WarpCoroutine(Player _player, Vector3 _mouseVec, float _maxRayDistance, int _rayHitLayerMask)
    // {
    //     yield return new WaitForSeconds(0.4f);//ちゅ！！マジックナンバーでごめん♡
    //     PlayerMove(_player, _mouseVec, _maxRayDistance,_rayHitLayerMask);
    // }

    public void PlayerMove(Player _player, Vector3 _mouseVec, float _maxRayDistance, int _rayHitLayerMask, float _playerMoveCorrection)
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
            _player.transform.position = hitInfo.point + hitInfo.normal * _playerMoveCorrection;
            _player.transform.rotation = Quaternion.LookRotation(-Vector3.up, hitInfo.normal);
        }
    }
}
