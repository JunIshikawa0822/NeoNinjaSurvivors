using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

//変数のみを格納するクラス
[System.Serializable]
public class GameStatus
{
    [Header("player")]
    [SerializeField] public Player player;

    [SerializeField]
    public LayerMask playerLayer;

    [System.NonSerialized]
    public bool isPlayerDamage = false;

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

    [Header("Bullet")]
    [SerializeField] public Bullet bullet;
    [System.NonSerialized] public List<Bullet> bulletList = new List<Bullet>();

    [Header("ReflectBullet")]
    [SerializeField] public ReflectBullet reflectBullet;
    [System.NonSerialized] public List<ReflectBullet> reflectBulletList = new List<ReflectBullet>();

    [Header("LineRenderer")]
    [SerializeField]
    public LineRenderer playerLineRenderer;

    [Range(1, 5)]
    public float lineStartDistance = 1f;

    [Range(2, 15)]
    public float lineMaxDistance = 5f;

    [Header("Input")]
    [System.NonSerialized]
    public bool isMoveInput = false;

    [System.NonSerialized]
    public bool isAttackInput = false;

    [System.NonSerialized]
    public bool isFootHoldInput = false;

    [System.NonSerialized]
    public bool isMoveInputUp = false;

    [System.NonSerialized]
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

    [Header("PlayerMove")]
    [SerializeField]
    public LayerMask playerMoveRayHitLayer;

    [System.NonSerialized]
    public float playerMoveMaxDistance = 20;

    [Header("Foothold")]
    [SerializeField]
    public GameObject footholdObject;

    [Range(3, 10), SerializeField]
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
    public PlayerObjectData playerObjectData;
    public BulletObjectData bulletObjectData;
    public ReflectBulletObjectData reflectBulletObjectData;

    public List<EnemyData> enemyDataList;

    [Header("Debug")]
    public List<GameObject> checkObjectList = new List<GameObject>();

    public enum InputNameType
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift,
        left_shift
    }
}
