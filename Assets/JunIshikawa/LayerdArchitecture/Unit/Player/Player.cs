using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    //animation用ステータス
    //private bool normalWaitAnimParam;
    private bool moveWaitAnimParam;
    private bool attackAnimParam;

    public void Init(bool _moveWaitAnimParam, bool _attackAnimParam)
    {
        ParameterSet(_moveWaitAnimParam, _attackAnimParam);
    }

    public void OnUpdate()
    {
        ParamSetToAnimator();
    }

    private void ParamSetToAnimator()
    {
        entityAnimator.SetBool("isWait", moveWaitAnimParam);
        entityAnimator.SetBool("isAttack", attackAnimParam);
    }

    public void ParameterSet(bool _moveWaitAnimParam, bool _attackAnimParam)
    {
        //normalWaitAnimParam = _normalWaitAnimParam;
        moveWaitAnimParam = _moveWaitAnimParam;
        attackAnimParam = _attackAnimParam;
    }
}
