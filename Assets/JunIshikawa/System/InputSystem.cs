using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SystemBase, IOnPreUpdate
{
    public void OnPreUpdate()
    {
        MouseAttackInput();
    }

    private void GetMoveInput(bool _isMoveInput)
    {
        
    }

    private void GetAttackInput(bool _isAttackInput)
    {

    }

    private void GetFootHoldInput(bool _isHootHoldInput)
    {

    }

    private bool isInput(GameStatus.InputName _inputName)
    {
        bool isInput = false;
        int indexNum = (int)_inputName;

        //if (Input.GetButtonDown())
        //{

        //}
        return isInput;
    }

    //マウスを右クリックした時
    private void MouseAttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameStat.attackVector = gameStat.player.transform.up;
            //攻撃のインプットをtrue
            gameStat.isAttackInput = true;
        }
        else
        {
            //攻撃のインプットをfalse
            gameStat.isAttackInput = false;
        }
    }
}
