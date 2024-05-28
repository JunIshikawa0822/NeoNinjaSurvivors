using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public bool isMoveInput = false;//変数

    //[System.NonSerialized]
    [SerializeField]
    public bool isAttackInput = false;//変数

    //[System.NonSerialized]
    [SerializeField]
    public bool isFootHoldInput = false;//変数

    [SerializeField]
    public InputName moveInputName;//Scriptable行き

    [SerializeField]
    public InputName attackInputName;//Scriptable行き

    [SerializeField]
    public InputName footHoldInputName;//Scriptable行き

    [System.NonSerialized]
    public Vector3 attackVector;//削除

    [System.NonSerialized]
    public Vector3 playerMouseVector;//変数

    [Header("PlayerMove")]
    [System.NonSerialized]
    public float playerMoveMaxDistance = 20;//Scriptable行き

    [SerializeField]
    public LayerMask playerMoveRayHitLayer;//Scriptable行き

    [Header("Level")]
    //レベル1から2に必要な経験値（初項）
    [Range(3, 10), SerializeField]
    public int playerPrimeDemandExp = 3;//Scriptable行き

    //レベルが上がるにつれて、レベルアップまでに必要な経験値を増やすための公比
    [Range(1, 2), SerializeField]
    public float playerExpRatio = 1.5f;//Scriptable行き

    [SerializeField]
    public int playerTotalExp = 0;//変数

    [System.NonSerialized]
    public int playerLevel = 0;//変数

    [System.NonSerialized]
    public int expSliderMaxValue;//変数

    [System.NonSerialized]
    public int expSliderProgressValue;//変数

    [System.NonSerialized]
    public int accumeExpUntilNowLevel;

    [System.NonSerialized]
    public int playerPreLevel = 1;//変数

    [System.NonSerialized]
    public bool isLevelUp = false;//変数

    [Header("UI")]
    [SerializeField]
    public Slider playerExpSlider;//アタッチ

    [SerializeField]
    public TextMeshProUGUI playerLevelText;//アタッチ

    public enum InputName//Scriptable行き
    {
        MouseButtonRight,
        MouseButtonLeft,
        space,
        right_shift,
        left_shift
    }

}
