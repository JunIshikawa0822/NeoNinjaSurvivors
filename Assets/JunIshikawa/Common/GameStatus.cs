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

    [Range(1, 7)] 
    //貫通力レベル
    public int penetrateCount = 1;

    [Range(1, 7)]
    //同時発射数レベル
    public int simulNumLevel = 1;

    [Range(1, 7)]
    //同時発射角度レベル
    public int angleLevel = 1;

    [System.NonSerialized]
    //レベルに応じた角度の設定
    public int[] bulletAngleLevelArray = new int[] { 0 , 5, 7, 10, 12, 15, 17, 20 };

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

    [System.NonSerialized]
    public Vector3 playerMouseVector;

    [Header("PlayerMove")]
    [System.NonSerialized]
    public float playerMoveMaxDistance = 20;

    [SerializeField]
    public LayerMask playerMoveRayHitLayer;

    [Header("Level")]
    //レベル1から2に必要な経験値（初項）
    [Range(3, 10), System.NonSerialized]
    public int playerPrimeDemandExp = 3;

    //レベルが上がるにつれて、レベルアップまでに必要な経験値を増やすための公比
    [Range(1, 2), System.NonSerialized]
    public float playerExpRatio = 1.5f;

    [System.NonSerialized]
    public int playerTotalExp = 0;

    [System.NonSerialized]
    public int playerLevel = 0;

    [System.NonSerialized]
    public int barMaxValue;

    [System.NonSerialized]
    public int barProgressValue;

    [System.NonSerialized]
    public int playerPreLevel = 0;

    [System.NonSerialized]
    public bool isLevelUp = false;


    public enum InputName
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift,
        left_shift
    }

}
