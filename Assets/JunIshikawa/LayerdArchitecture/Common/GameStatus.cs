using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System;

//変数のみを格納するクラス
[System.Serializable]
public class GameStatus
{
    [Header("player")]
    [SerializeField] public Player player;
    [SerializeField] public LayerMask playerLayer;

    [Header("Enemy")]
    [SerializeField] public EyeballEnemy eyeballEnemy;
    [SerializeField] public GreenEnemy greenEnemy;
    [SerializeField] public ArmEnemy armEnemy;
    [SerializeField] public BlackEnemy blackEnemy;
    [SerializeField] public FlyingEnemy flyingEnemy;
    [SerializeField] public GoldEnemy goldEnemy;

    public enum EnemyType
    {
        EyeBallEnemy,
        GreenEnemy,
        ArmEnemy,
        BlackEnemy,
        FlyingEnemy,
        GoldEnemy
    }

    [System.NonSerialized]
    public List<EnemyBase> enemyList = new List<EnemyBase>();

    [Header("EnemyGenerate(Change Difficulty)")]
    [SerializeField] public float spawnInterval;

    [SerializeField] public float spawnRadius;

    //左からEye,Green,Arm,Black,Flying,Goldの各生成確率（毎秒ごとに更新する）
    [SerializeField] public float[] spawnProbabilities = new float[] {100,50,0,0,0,0};

    public float elapsedTime = 0f;

    [SerializeField] public int spawnLimitNum  = 50;

    [Header("Bullet")]
    
    [SerializeField] public Bullet bullet;
    public List<Bullet> bulletList = new List<Bullet>();
    public bool isBulletUsing = false;

    [Header("ReflectBullet")]
    [SerializeField] public ReflectBullet reflectBullet;
    public bool isReflectBulletUsing = false;
    
    public List<ReflectBullet> reflectBulletList = new List<ReflectBullet>();

    [Header("Shuriken")]
    
    [SerializeField] public Shuriken shuriken;
    public List<Shuriken> shurikenList = new List<Shuriken>();

    [Header("BigShuriken")]
    [SerializeField] public BigShuriken bigShuriken;

    [Header("AutoBullet")]
    [SerializeField] public AutoBullet autoBullet;
    public List<AutoBullet> autoBulletList = new List<AutoBullet>();
    [System.NonSerialized] public float nextFireTime;
    [System.NonSerialized] public int currentEnemyIndex = 0; // 現在の発射対象のインデックス
    [SerializeField] public List<EnemyBase> currentTargetEnemies = new List<EnemyBase>();

    [Header("Aura")]
    
    [SerializeField] public Aura aura;

    [SerializeField] public bool isAuraUsing = false;
    [System.NonSerialized] public Aura activeAuraInstance = null;
    [System.NonSerialized] public Dictionary<EnemyBase, float> enemyTimers = new Dictionary<EnemyBase, float>();

    [Header("Thunder")]
    
    [SerializeField] public Thunder thunder;
    [SerializeField] public bool isThunderUsing = false;

    [Header("LineRenderer")]
    [SerializeField]
    public LineRenderer playerLineRenderer;

    [Range(-1, 5)]
    public float lineStartDistance = 1f;

    [Range(2, 15)]
    public float lineMaxDistance = 5f;

    [Header("Input")]
    [SerializeField]
    public bool isMoveInput = false;

    //[System.NonSerialized]
    [SerializeField]
    public bool isAttackInput = false;

    //[System.NonSerialized]
    [SerializeField]
    public bool isFootHoldInput = false;

    [SerializeField]
    public bool isFootHoldInputUp = false;

    [SerializeField]
    public InputNameType moveInputName;

    [SerializeField]
    public InputNameType attackInputName;

    [SerializeField]
    public InputNameType footHoldInputName;

    [System.NonSerialized]
    public Vector3 attackVector;

    [System.NonSerialized]
    public Vector3 playerMouseVector;

    [Header("Animation")]
    [SerializeField]
    public bool isMoveInputUp = false;

    [Header("PlayerMove")]
    [SerializeField]
    public LayerMask playerMoveRayHitLayer;

    [System.NonSerialized]
    public float playerMoveMaxDistance = 20;

    [SerializeField]
    public float playerMoveCorrection = 1.6f;//プレイヤー位置の補正値

    [Header("FootHold")]
    [SerializeField]
    public GameObject footholdObject;
    [SerializeField]
    public GameObject footholdPredictObject;

    [Range(1,5),SerializeField]
    public float footholdSetDistance;

    [Header("Level")]
    [Range(3, 10), SerializeField]
    //レベル1から2に必要な経験値（初項）
    public int playerPrimeDemandExp = 3;

    //レベルが上がるにつれて、レベルアップまでに必要な経験値を増やすための公比
    [Range(1, 2), System.NonSerialized]
    public float playerExpRatio = 1.5f;

    [SerializeField]
    public int playerTotalExp = 0;

    [System.NonSerialized]
    public int playerLevel = 1;

    [System.NonSerialized]
    public int accumeExpUntilNowLevel;

    [System.NonSerialized]
    public int playerExpSliderMaxValue;

    [System.NonSerialized]
    public int playerExpSliderProgressValue;

    [System.NonSerialized]
    public int playerPreLevel = 0;

    [SerializeField]
    public bool isLevelUp = false;

    [Header("UI")]
    [SerializeField]
    public TextMeshProUGUI playerLevelText;

    [SerializeField]
    public Slider playerExpSlider;

    [SerializeField]
    public Slider playerHpSlider;

    [SerializeField]
    public GameObject DebugButton;

    [SerializeField]
    public TextMeshProUGUI timerText;

    [Header("Reward")]
    [SerializeField]
    public List<Texture2D> rewardTextures = new List<Texture2D>(); 
    public List<String> currentRewardsSet = new List<String>();
    [SerializeField] public int thunderSkillLevel = 1;
    [SerializeField] public int auraSkillLevel = 1;
    [SerializeField] public int shurikenSkillLevel = 1;
    [SerializeField] public int bulletSkillLevel = 1;
    [SerializeField] public int refelectBulletLevel = 1;
    [System.NonSerialized] public bool isLevelUpOnce = false;


    [Header("Timer")]
    [System.NonSerialized]
    public float seconds;

    [System.NonSerialized]
    public float oldSeconds;

    [System.NonSerialized]
    public int minutes;

    [Header("LevelUpPanel")]
    [SerializeField]
    public GameObject levelUpPanel;

    [SerializeField]
    public GameObject levelUpEnterButton;

    [SerializeField]
    public List<GameObject> selectPanelsList;

    [System.NonSerialized]
    public int selectedPanelNumber = 0;

    [System.NonSerialized]
    public bool isPanelSelected = false;

    [Header("Data")]
    public List<EnemyData> enemyDataList;
    public PlayerObjectData playerObjectData;
    public BulletObjectData bulletObjectData;
    public ReflectBulletObjectData reflectBulletObjectData;
    public AutoBulletObjectData autoBulletObjectData;
    public ThunderObjectData thunderObjectData;
    public AuraObjectData auraObjectData;

    [Header("AutoAttack")]
    public float attackRange;
    public float coolTime = 3;

    public enum InputNameType
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift,
        left_shift
    }
}
