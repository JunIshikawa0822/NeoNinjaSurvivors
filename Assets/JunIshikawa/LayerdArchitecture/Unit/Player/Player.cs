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
    private bool isInvincible;
    private bool isOnLevelUp;

    private WaitForSeconds invincibleInterval;
    private float invincibleTime;

    

    public void Init(bool _attackAnimParam, float _invincibleTime)
    {
        ParameterSet(_attackAnimParam);
        isInvincible = false;
        isOnLevelUp = false;
        invincibleTime = _invincibleTime;

        invincibleInterval = new WaitForSeconds(invincibleTime);
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

    public void IsOnLevelUpSet(bool _isLevelUp)
    {
        isOnLevelUp = _isLevelUp;
    }

    public bool IsOnWarpEnable()
    {
        return isOnWarp;
    }

    public void IsOnWarpControl()
    {
        isOnWarp = !isOnWarp;
    }
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

    public void PlayerGetDamage(int _damagePoint){

        if (isInvincible)
        {
            Debug.Log("無敵");
            return;
        }

        base.EntityGetDamage(_damagePoint);
        StartCoroutine(InvincibleTime(invincibleTime));
        //Player_Damage_Effect
        //Debug.Log("DamagePoint : " + _damagePoint);
    }

    private IEnumerator InvincibleTime(float _time)
    {
        if (!isOnLevelUp)
        {
            isInvincible = true;
        }
        
        yield return invincibleInterval;

        isInvincible = false;
    }
}
