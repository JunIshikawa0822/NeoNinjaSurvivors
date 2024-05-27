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

    [System.NonSerialized] 
    public int bulletDamage = 1;

    [System.NonSerialized] 
    public int penetrateCount = 1;

    [Header("Input")]
    //[System.NonSerialized]
    [SerializeField]
    public bool isMoveInput = false;

    //[System.NonSerialized]
    [SerializeField]
    public bool isAttackInput = false;

    //[System.NonSerialized]
    [SerializeField]
    public bool isFootHoldInput = false;

    [SerializeField]
    public InputName moveInputName;

    [SerializeField]
    public InputName attackInputName;

    [SerializeField]
    public InputName footHoldInputName;

    [System.NonSerialized]
    public Vector3 attackVector;

    public enum InputName
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift
    }

}
