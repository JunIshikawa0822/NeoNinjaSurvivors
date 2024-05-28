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
            new MoveSystem(),
            new EnemySystem(),
            new InputSystem(),
            new AttackSystem(),
            new LevelSystem(),
            new UISystem()
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
    private void Start()
    {
        foreach (SystemBase system in allSystems) system.SetUp();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (IOnPreUpdate system in allPreUpdateSystems) system.OnPreUpdate();
        foreach (IOnUpdate system in allUpdateSystems) system.OnUpdate();   
    }

    private void LateUpdate()
    {
        foreach (IOnLateUpdate system in allLateUpdateSystems) system.OnLateUpdate();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 260, 10, 250, 150), "EXP & LEVEL");
        GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "playerlevel : " + gameStat.playerLevel.ToString());
        GUI.Label(new Rect(Screen.width - 245, 40, 250, 30), "accume exp to spec level : " + gameStat.accumeExpUntilNowLevel.ToString());
        GUI.Label(new Rect(Screen.width - 245, 50, 250, 30), "barProgress : " + gameStat.expSliderProgressValue.ToString() + "/" + gameStat.expSliderMaxValue.ToString());
    }
}
