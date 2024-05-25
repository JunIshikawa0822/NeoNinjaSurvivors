using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SystemBase, IOnPreUpdate
{
    public void OnPreUpdate()
    {
        //MouseAttackInput();
        //gameStat.attackVector = MouseVec(gameStat.player.transform, Camera.main, Input.mousePosition, gameStat.player);
        gameStat.attackVector = RestrictVector(gameStat.player.transform, MouseVec(gameStat.player.transform, Camera.main, Input.mousePosition, gameStat.player), 150);

        GetMoveInput(gameStat.moveInputName);
        GetAttackInput(gameStat.attackInputName);
        GetFootHoldInput(gameStat.footHoldInputName);
    }

    //押されたキーがMoveのキーならMoveをオンにする
    private void GetMoveInput(GameStatus.InputName _isMoveInput)
    {
        gameStat.isMoveInput = IsInput(_isMoveInput);
        //Debug.Log("JunIshikawa");
    }

    //押されたキーがAttackのキーならAttackをオンにする
    private void GetAttackInput(GameStatus.InputName _isAttackInput)
    {
        gameStat.isAttackInput = IsInput(_isAttackInput);
        //Debug.Log("JunIshikawa");
    }

    //押されたキーがFootHoldのキーならFootHoldをオンにする
    private void GetFootHoldInput(GameStatus.InputName _isFootHoldInput)
    {
        gameStat.isFootHoldInput = IsInput(_isFootHoldInput);
        //Debug.Log("JunIshikawa");
    }

    //引数のキー（マウスボタン）が押されたかどうかを確認
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
        Debug.Log(Angle);

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
