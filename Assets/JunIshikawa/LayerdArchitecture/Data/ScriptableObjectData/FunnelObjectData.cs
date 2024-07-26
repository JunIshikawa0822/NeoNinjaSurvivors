using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FunnelData", menuName = "ScriptableObject/FunnelData", order = 0)]
public class FunnelObjectData : ScriptableObject
{
    [SerializeField]
    public float radius;

    [SerializeField]
    public float theta;

    [SerializeField]
    public float moveMaxDistance;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int simulNum;

    [SerializeField]
    public float coolTime;

    [SerializeField]
    public int damage;
}
