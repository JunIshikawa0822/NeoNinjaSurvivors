using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigShurikenData", menuName = "ScriptableObject/BigShurikenData", order = 0)]
public class BigShurikenObjectData : ScriptableObject
{
    [SerializeField] public bool isBSUsing = false;
    // [SerializeField] public float auraRadius = 5f;
    // [System.NonSerialized] public float elapsedAuraTime = 0f;
    // [SerializeField] public float auraNockBackInterval = 3f;
    // [SerializeField] public float auraNockBackStrength = 5f;
    // [SerializeField] public int auraDamage = 1;
}
