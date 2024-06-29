using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate(){
        if (gameStat.isAuraUsing && gameStat.activeAuraInstance == null) {
            AuraActivation(gameStat.aura,ref gameStat.activeAuraInstance, gameStat.player.transform.position);
        } else if (!gameStat.isAuraUsing && gameStat.activeAuraInstance != null) {
            AuraDeActivation(ref gameStat.activeAuraInstance);
        }
    }

    private void AuraActivation(Aura _aura,ref Aura _activeAuraInstance, Vector3 _playerPos){
        Debug.Log("Aura発動！！");
        _activeAuraInstance = GameObject.Instantiate(_aura, _playerPos, Quaternion.identity);
    }

    private void AuraDeActivation(ref Aura _activeAuraInstance){
        _activeAuraInstance.AuraDestroy();
        _activeAuraInstance = null;
    }
}
