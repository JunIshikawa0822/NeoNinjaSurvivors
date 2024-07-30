using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    private Transform funnelRotateCenter;
    private float radius;
    private float theta;
    private float moveDistance;
    private float maxDistance;
    private float speed;

    private int funnelDamage;

    //位置補正
    public event Action onFunnelUpdate;

    //orbitListからの削除を担当
    public event Action<Funnel> funnelRemoveEvent;

    public event Action<Collider, Funnel> funnelCollideEvent;

    public void Init(Transform _funnelRotateCenter, float _radius, float _theta, float _maxDistance, float _speed, int _damage)
    {
        funnelRotateCenter = _funnelRotateCenter;
        radius = _radius;
        theta = _theta;
        maxDistance = _maxDistance;
        speed = _speed;
        funnelDamage = _damage;
    }

    public void OnUpdate()
    {
        OnFunnelThetaUpdate();
        Move();
    }

    private void Move()
    {
        theta += Time.deltaTime * speed;
        moveDistance += Time.deltaTime;

        transform.position = funnelRotateCenter.position + new Vector3(
            radius * Mathf.Cos(theta /* Mathf.Deg2Rad*/),
            /*orbitCenter.transform.lossyScale.y / 2*/0,
            radius * Mathf.Sin(theta /* Mathf.Deg2Rad*/)
            );

        if (moveDistance > /*3 * Mathf.PI*/ maxDistance / 3 * Mathf.PI)
        {
            OnTriggerNextAction();
            FunnelDestroy();
        }
    }

    public int FunnelDamage()
    {
        return funnelDamage;
    }

    private void OnFunnelThetaUpdate()
    {
        if (onFunnelUpdate == null) return;
        onFunnelUpdate?.Invoke();
    }

    public float GetTheta()
    {
        return theta;
    }

    public void SetTheta(float _theta)
    {
        theta = _theta;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (funnelCollideEvent == null) return;
        funnelCollideEvent?.Invoke(_collider, this);
    }

    public void FunnelDestroy()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerNextAction()
    {
        if (funnelRemoveEvent == null) return;
        funnelRemoveEvent?.Invoke(this);
    }
}
