using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderObjectData", menuName = "ScriptableObject/ThunderObjectData", order = 0)]
public class ThunderObjectData : ScriptableObject
{
    
    //落雷範囲
    public float thunderRange = 10f;
    //落雷発生間隔
    public float thunderInterval = 5f;
    //攻撃力
    public int thunderDamage = 5;
    //落雷タイマー
    [System.NonSerialized]public float thunderTime = 0f;
    //ノックバックの強さ
    public float thunderNockBackStrength = 5f;
}
