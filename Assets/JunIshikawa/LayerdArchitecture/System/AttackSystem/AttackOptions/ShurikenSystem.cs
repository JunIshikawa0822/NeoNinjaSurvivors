using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate() {
        if (gameStat.shurikenList.Count > 0)
        {
            for (int i = gameStat.shurikenList.Count - 1; i >= 0; i--)
            {
                gameStat.shurikenList[i].OnUpdate();
            }
        }

        
    }


}
