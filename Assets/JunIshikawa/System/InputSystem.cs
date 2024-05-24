using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SystemBase, IOnPreUpdate
{
    public void OnPreUpdate()
    {
        //MouseAttackInput();

        GetMoveInput(gameStat.moveInputName);
        GetAttackInput(gameStat.attackInputName);
        GetFootHoldInput(gameStat.footHoldInputName);
    }

    //押されたキーがMoveのキーならMoveをオンにする
    private void GetMoveInput(GameStatus.InputName _isMoveInput)
    {
        gameStat.isMoveInput = IsInput(_isMoveInput);
        Debug.Log("JunIshikawa");
    }

    //押されたキーがAttackのキーならAttackをオンにする
    private void GetAttackInput(GameStatus.InputName _isAttackInput)
    {
        gameStat.isAttackInput = IsInput(_isAttackInput);
        Debug.Log("JunIshikawa");
    }

    //押されたキーがFootHoldのキーならFootHoldをオンにする
    private void GetFootHoldInput(GameStatus.InputName _isFootHoldInput)
    {
        gameStat.isFootHoldInput = IsInput(_isFootHoldInput);
        Debug.Log("JunIshikawa");
    }

    //引数のキー（マウスボタン）が押されたかどうかを確認
    private bool IsInput(GameStatus.InputName _inputName)
    {
        Debug.Log("JunIshikawa");
        bool isInputBool = false;

        if (_inputName == GameStatus.InputName.MouseButtonRight)
        {
            isInputBool = MouseBool(1);
        }
        else if (_inputName == GameStatus.InputName.MouseButtonLeft)
        {
            isInputBool = MouseBool(0);
        }
        else
        {
            isInputBool = KeyBool(_inputName.ToString());
        }

        return isInputBool;

        bool MouseBool(int _mouseNumber)
        {
            if (Input.GetMouseButtonDown(_mouseNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool KeyBool(string _keyName)
        {
            string[] words = _keyName.Split("_");
            string conventionKeyName = string.Join(" ", words);

            if (Input.GetKeyDown(conventionKeyName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    private void MouseAttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameStat.attackVector = gameStat.player.transform.up;
            //isAttackInputをtrueにするよ
            gameStat.isAttackInput = true;
        }
        else
        {
            //isAttackInputをfalseにするよ
            gameStat.isAttackInput = false;
        }
    }
}
