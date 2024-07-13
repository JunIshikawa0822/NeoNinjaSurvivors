using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReflectBulletData", menuName = "ScriptableObject/ReflectBulletData", order = 0)]
public class ReflectBulletObjectData : ScriptableObject
{
    [SerializeField]
    public float bulletSpeed = 0.2f;

    [SerializeField]
    public float maxDistance = 100f;

    [SerializeField]
    public int bulletDamage = 1;

    [Range(1, 7), SerializeField]
    //貫通力レベル
    public int penetrateCount = 1;

    [Range(1, 7), SerializeField]
    //同時発射数レベル
    public int simulNumLevel = 1;

    [Range(1, 7), SerializeField]
    //同時発射角度レベル
    public int angleLevel = 1;

    [System.NonSerialized]
    //レベルに応じた角度の設定
    public int[] bulletAngleLevelArray = new int[] { 0, 5, 7, 10, 12, 15, 17, 20 };

    [System.NonSerialized]
    public float coolTime = 1;
}
