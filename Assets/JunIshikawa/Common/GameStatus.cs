using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//変数のみを格納するクラス
[System.Serializable]
public class GameStatus
{
    [Header("Input")]
    [System.NonSerialized]
    public bool isMoveInput = false;

    [System.NonSerialized]
    public bool isAttackInput = false;

    [System.NonSerialized]
    public bool isFootHoldInput = false;

    [SerializeField]
    public InputName moveInput;

    [SerializeField]
    public InputName attackInput;

    [SerializeField]
    public InputName hootHoldInput;

    public enum InputName
    {

        Space,
        RightShift,
    }
}
