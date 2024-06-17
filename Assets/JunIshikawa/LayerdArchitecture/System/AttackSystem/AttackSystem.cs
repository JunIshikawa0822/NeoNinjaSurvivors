using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : SystemBase, IOnUpdate
{
    List<AttackSystemBase> allAttackOptionsList;
    List<IOnUpdate> allUpdateAttackOptionsList;

    public override void SetUp()
    {
        allAttackOptionsList = new List<AttackSystemBase>()
        {
            new BulletSystem()
        };

        allUpdateAttackOptionsList = new List<IOnUpdate>();

        foreach (AttackSystemBase attackOption in allAttackOptionsList)
        {
            //GameStatの値をSystemたちに全て読み込ませる　これによりSystemたちはGameStatusの値全てを自由に使うことができる
            attackOption.GameStatusInit(gameStat);
            //SystemたちをallSystemに一旦ぶち込んだ後で、それぞれのInterfaceに従って分類する
            if (attackOption is IOnUpdate) allUpdateAttackOptionsList.Add(attackOption as IOnUpdate);
        }

        foreach (AttackSystemBase attackOption in allAttackOptionsList) attackOption.AttackOptionSetUp();
    }

    public void OnUpdate()
    {
        if (gameStat.isLevelUp) return;
        foreach (IOnUpdate attackOption in allUpdateAttackOptionsList) attackOption.OnUpdate();
    }
}
