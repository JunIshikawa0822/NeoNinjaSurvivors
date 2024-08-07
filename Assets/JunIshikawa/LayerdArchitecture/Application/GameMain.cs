using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameStatus gameStat;

    List<SystemBase> allSystems;

    List<IOnUpdate> allUpdateSystems;
    List<IOnPreUpdate> allPreUpdateSystems;
    List<IOnLateUpdate> allLateUpdateSystems;

    private void Awake()
    {
        allSystems = new List<SystemBase>()
        {
            new PlayerSystem(),
            new EnemySystem(),
            new InputSystem(),
            new AttackSystem(),
            new DrawSystem(),
            new LevelSystem(),
            new UISystem(),
            new RewardSystem(),
            new TimeSystem(),
            new FootholdSystem()
        };

        allUpdateSystems = new List<IOnUpdate>();
        allPreUpdateSystems = new List<IOnPreUpdate>();
        allLateUpdateSystems = new List<IOnLateUpdate>();

        foreach (SystemBase system in allSystems)
        {
            //GameStatの値をSystemたちに全て読み込ませる　これによりSystemたちはGameStatusの値全てを自由に使うことができる
            system.GameStatusInit(gameStat);

            //SystemたちをallSystemに一旦ぶち込んだ後で、それぞれのInterfaceに従って分類する
            //if (system is IOnFixedUpdate) allFixedUpdateSystems.Add(system as IOnFixedUpdate);
            if (system is IOnPreUpdate) allPreUpdateSystems.Add(system as IOnPreUpdate);
            if (system is IOnUpdate) allUpdateSystems.Add(system as IOnUpdate);
            if (system is IOnLateUpdate) allLateUpdateSystems.Add(system as IOnLateUpdate);
        }

        //Debug.Log(string.Join(",", allUpdateSystems));
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (SystemBase system in allSystems) system.SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (IOnPreUpdate system in allPreUpdateSystems) system.OnPreUpdate();
        foreach (IOnUpdate system in allUpdateSystems) system.OnUpdate();
    }

    void LateUpdate()
    {
        foreach (IOnLateUpdate system in allLateUpdateSystems) system.OnLateUpdate();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 260, 10, 250, 150), "Debug");
        GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "playerLevel : " + gameStat.playerLevel.ToString());
        GUI.Label(new Rect(Screen.width - 245, 40, 250, 30), "progress : " + gameStat.playerExpSliderProgressValue.ToString() + "/" + gameStat.playerExpSliderMaxValue.ToString());

    }
}
