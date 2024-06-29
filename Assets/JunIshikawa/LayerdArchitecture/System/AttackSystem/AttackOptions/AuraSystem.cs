using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSystem : AttackOptionBase, IOnUpdate
{
    public void OnUpdate(){
        if (gameStat.isAuraUsing && gameStat.activeAuraInstance == null) {
            AuraActivation(gameStat.aura, gameStat.activeAuraInstance, gameStat.player.transform.position);
        } else if (!gameStat.isAuraUsing && gameStat.activeAuraInstance != null) {
            
        }
    }

    private void AuraActivation(Aura _aura,Aura _activeAuraInstance, Vector3 _playerPos){
        _activeAuraInstance = GameObject.Instantiate(_aura, _playerPos, Quaternion.identity);
    }

    private void AuraDeActivation(Aura _aura,Aura _activeAuraInstance, Vector3 _playerPos){
        //Destroy(activeAuraInstance.gameObject);
        _activeAuraInstance = null;
    }
}
