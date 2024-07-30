using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigShurikenData", menuName = "ScriptableObject/BigShurikenData", order = 0)]
public class BigShurikenObjectData : ScriptableObject
{
    [SerializeField] public bool isBSUsing = false;
}
