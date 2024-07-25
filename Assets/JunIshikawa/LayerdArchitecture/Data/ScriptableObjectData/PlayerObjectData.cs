using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = 0)]
public class PlayerObjectData : ScriptableObject
{
    [SerializeField]
    public int playerMaxHp;

    [SerializeField]
    public float invincibleTime;
}

