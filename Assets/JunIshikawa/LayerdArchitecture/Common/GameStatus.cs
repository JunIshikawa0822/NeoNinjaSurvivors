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
    public List<Bullet> bulletList = new List<Bullet>();

    [SerializeField] 
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

    [Header("LineRenderer")]
    [SerializeField]
    public LineRenderer playerLineRenderer;

    [Range(1, 5)]
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

    [Header("LevelUpPanel")]
    [SerializeField]
    public GameObject levelUpPanel;

    [SerializeField]
    public GameObject levelUpEnterButton;

    [SerializeField]
    public List<GameObject> selectPanelsList;

    [System.NonSerialized]
    public int selectedPanelNumber = 0;

    [Header("Data")]
    public List<EnemyData> enemyDataList;
    public List<PlayerData> playerDataList;

    public enum InputNameType
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift,
        left_shift
    }
}
