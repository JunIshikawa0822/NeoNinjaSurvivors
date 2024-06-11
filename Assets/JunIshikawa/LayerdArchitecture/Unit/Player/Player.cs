using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public void Init()
    {

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
}
