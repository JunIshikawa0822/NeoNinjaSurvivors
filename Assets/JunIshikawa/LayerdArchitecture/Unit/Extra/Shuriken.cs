using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuriken : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveDistance = 0;
    private float maxDistance;
    private float shurikenSpeed;
    private int shurikenDamage;//弾丸のダメージ量

    //弾丸がListから削除されるAction
    public event Action<Shuriken> shurikenRemoveEvent;
    //
    public event Action<Collider, Shuriken> shurikenCollideEvent;

    public void Init(Vector3 _moveDir, float _shurikenSpeed, float _maxDistance, int _shurikenDamage)
    {
        //attackVectorが代入される
        this.moveDir = _moveDir;
        this.shurikenSpeed = _shurikenSpeed;
        this.maxDistance = _maxDistance;
        this.shurikenDamage = _shurikenDamage;

        RotateSet(moveDir);
    }

    public void OnUpdate()
    {
        if (moveDistance > maxDistance)
        {
            OnTriggerNextAction();//リストから削除
            ShurikenDestroy();//オブジェクトを破壊
        }
        else
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 moveValue = moveDir * shurikenSpeed;
        transform.position += moveValue;
        moveDistance += moveValue.magnitude;
    }

    private void Rotate()
    {
        this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 20, 0);
    }

    //弾丸のダメージを返す
    public int ShurikenDamage()
    {
        return shurikenDamage;
    }

    //弾丸が消えるときに起きるイベント
    public void OnTriggerNextAction()
    {
        if (shurikenRemoveEvent == null) return;
        shurikenRemoveEvent?.Invoke(this);
    }

    //弾丸が破壊
    public void ShurikenDestroy()
    {
        Destroy(this.gameObject);
    }

    //弾丸の衝突
    private void OnTriggerEnter(Collider _collider)
    {
        Debug.Log("衝突");
        if (shurikenCollideEvent == null) return;
        shurikenCollideEvent?.Invoke(_collider, this);
    }

    private void RotateSet(Vector3 _directionVec)
    {
        Vector3 angles = transform.localEulerAngles;
        float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
        angles.y = -angle - 180;
        transform.localEulerAngles = angles;
    }

    //private float VecToAngle(Vector3 _directionVec)
    //{
    //    float angle = Mathf.Atan2(_directionVec.z, _directionVec.x) * Mathf.Rad2Deg;
    //    Debug.Log(angle);
    //    return angle;
    //}
}
