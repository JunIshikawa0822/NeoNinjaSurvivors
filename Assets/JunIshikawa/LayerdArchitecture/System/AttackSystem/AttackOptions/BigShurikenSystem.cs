using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShurikenSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate(){
        gameStat.bigShuriken.OnUpdate();
    }
}
