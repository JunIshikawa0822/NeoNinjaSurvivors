using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Thunder : MonoBehaviour
{
    private int thunderDamage;
    public event Action<Collider , Thunder> thunderTriggerEvent;

    public void Init(int _thunderDamage)
    {
        this.thunderDamage = _thunderDamage;
    }

    public void ThunderDestroy(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider _other){
        if (thunderTriggerEvent == null) return;
        thunderTriggerEvent?.Invoke(_other , this);
    }

    public int ThunderDamage()
    {
        return thunderDamage;
    }
}
