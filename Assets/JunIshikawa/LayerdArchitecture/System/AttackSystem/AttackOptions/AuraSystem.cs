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
        } else if(gameStat.isAuraUsing && gameStat.activeAuraInstance != null) {
            AuraScale(ref gameStat.activeAuraInstance,gameStat.auraRadius, gameStat.player.transform.position);
            gameStat.elapsedAuraTime += Time.deltaTime;
            if(gameStat.elapsedAuraTime >= gameStat.auraNockBackInterval) {
                //ここで全ての射程圏内の敵にノックバックを与える
                AuraProcess(gameStat.activeAuraInstance, gameStat.auraRadius, gameStat.auraNockBackStrength);
                gameStat.elapsedAuraTime = 0f;
            }
        }

        
    }

    private void AuraScale(ref Aura _activeAuraInstance, float _auraRadius, Vector3 _playerPos){
        _activeAuraInstance.transform.localScale = new Vector3(_auraRadius, 1, _auraRadius);
        _activeAuraInstance.transform.position = _playerPos;
    }

    private void AuraProcess(Aura _activeAuraInstance, float _auraRadius, float _auraStrength){
        // 指定された半径内のコライダーを検出
        Collider[] hitColliders = Physics.OverlapSphere(_activeAuraInstance.transform.position, _auraRadius/2);
        
        // 検出したコライダーに対して処理を行う
        foreach (var hitCollider in hitColliders)
        {
            // 例として、検出したオブジェクトにダメージを与える処理
            EnemyBase enemy = hitCollider.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.EnemyNockBack(_activeAuraInstance.transform.position,enemy.transform.position,_auraStrength);
            }
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
