using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//変数のみを格納するクラス
[System.Serializable]
public class GameStatus
{
    [Header("player")]
    [SerializeField] public Player player;

    [Header("Bullet")]
    [SerializeField] public Bullet bullet;
    public List<Bullet> bulletList = new List<Bullet>();

    [System.NonSerialized] 
    public float bulletSpeed = 0.2f;

    [System.NonSerialized] 
    public float maxDistance = 100f;

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

    [System.NonSerialized]
    public Vector3 attackVector;

    public enum InputName
    {

        Space,
        RightShift,
    }

}
