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
}
