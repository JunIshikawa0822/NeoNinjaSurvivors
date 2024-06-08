using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SystemBase, IOnPreUpdate
{
    public void OnPreUpdate()
    {
        //MouseAttackInput();
        //gameStat.attackVector = MouseVec(gameStat.player.transform, Camera.main, Input.mousePosition, gameStat.player);
        gameStat.playerMouseVector = RestrictVector(gameStat.player.transform, MouseVec(gameStat.player.transform, Camera.main, Input.mousePosition, gameStat.player), 150);

        //各種ボタンが押されたか
        //InputDown(gameStat.isMoveInput, gameStat.moveInputName);
        //InputDown(gameStat.isAttackInput, gameStat.attackInputName);
        //InputDown(gameStat.isFootHoldInput, gameStat.footHoldInputName);

        GetMoveInput(gameStat.moveInputName);
        GetAttackInput(gameStat.attackInputName);
        GetFootHoldInput(gameStat.footHoldInputName);

        //Debug.Log(gameStat.isMoveInput);

        //moveボタンが離されたか
        InputUp(gameStat.isMoveInputUp, gameStat.moveInputName);
        //Debug.Log(gameStat.isMoveInputUp);
    }

    //押されたキーがMoveのキーならMoveをオンにする
    private void GetMoveInput(GameStatus.InputName _isMoveInput)
    {
        gameStat.isMoveInput = IsInputDown(_isMoveInput);
    }

    //押されたキーがAttackのキーならAttackをオンにする
    private void GetAttackInput(GameStatus.InputName _isAttackInput)
    {
        gameStat.isAttackInput = IsInputDown(_isAttackInput);
    }

    //押されたキーがFootHoldのキーならFootHoldをオンにする
    private void GetFootHoldInput(GameStatus.InputName _isFootHoldInput)
    {
        gameStat.isFootHoldInput = IsInputDown(_isFootHoldInput);
    }

    //押下されたらオンにする
    private void InputDown(bool _inputDown, GameStatus.InputName _isInput)
    {
        _inputDown = IsInputDown(_isInput);
    }

    //離されたらオンにする
    private void InputUp(bool _inputUp, GameStatus.InputName _isInput)
    {
        _inputUp = IsInputUp(_isInput);
    }

    //引数のキー（マウスボタン）が押下されたかどうかを確認
    private bool IsInputDown(GameStatus.InputName _inputName)
    {
        //Debug.Log("JunIshikawa");
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

    //引数のキー（マウスボタン）が押下されたかどうかを確認
    private bool IsInput(GameStatus.InputName _inputName)
    {
        //Debug.Log("JunIshikawa");
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
            if (Input.GetMouseButton(_mouseNumber))
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

            if (Input.GetKey(conventionKeyName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //引数のキー（マウスボタン）が離されたかどうかを確認
    private bool IsInputUp(GameStatus.InputName _inputName)
    {
        //Debug.Log("JunIshikawa");
        bool isInputBool = false;

        if (_inputName == GameStatus.InputName.MouseButtonRight)
        {
            isInputBool = MouseUpBool(1);
        }
        else if (_inputName == GameStatus.InputName.MouseButtonLeft)
        {
            isInputBool = MouseUpBool(0);
        }
        else
        {
            isInputBool = KeyUpBool(_inputName.ToString());
        }

        return isInputBool;

        bool MouseUpBool(int _mouseNumber)
        {
            if (Input.GetMouseButtonUp(_mouseNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool KeyUpBool(string _keyName)
        {
            string[] words = _keyName.Split("_");
            string conventionKeyName = string.Join(" ", words);

            if (Input.GetKeyUp(conventionKeyName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //最終的に必要なmouseVec
    private Vector3 MouseVec(Transform _originTransform, Camera _camera, Vector3 _mousePos, Player _player)
    {
        return PlayerToPointVector(
            _originTransform,
            new Vector3(
                ConvertScreenToWorldPoint(_camera, _mousePos).x,
                _player.transform.position.y / 2,
                ConvertScreenToWorldPoint(_camera, _mousePos).z)
            );

        //マウスで指定したい方向
        Vector3 PlayerToPointVector(Transform _originTransform, Vector3 _point)
        {
            //プレイヤーから目的地へ向かうベクトルを標準化
            Vector3 playerToPointVector = (_point - _originTransform.position).normalized;

            return playerToPointVector;
        }

        //Screen座標をWorld座標に変換
        Vector3 ConvertScreenToWorldPoint(Camera _camera, Vector3 _vector)
        {
            //マウスの現在地を取得
            Vector3 pos = _vector;
            pos.z = 1;
            Vector3 convertedPoint = _camera.ScreenToWorldPoint(pos);
            //Debug.Log(mousePoint);
            return convertedPoint;
        }
    }

    private Vector3 RestrictVector(Transform _originTransform, Vector3 _baseVec, float _restrictAngle)
    {
        Vector3 restVec;
        Vector3 fromVec = Vector3.ProjectOnPlane(_originTransform.up, -_originTransform.forward);

        Vector3 toVec = Vector3.ProjectOnPlane(_baseVec, -_originTransform.forward);

        float Angle = Vector3.SignedAngle(fromVec, toVec, _originTransform.up);
        //Debug.Log(Angle);

        if (Mathf.Abs(Angle) > _restrictAngle)
        {
            restVec = Quaternion.Euler(0, Mathf.Sign(Angle) * _restrictAngle, 0) * _originTransform.forward;
        }
        else
        {
            restVec = _baseVec;
        }

        return restVec.normalized;
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
