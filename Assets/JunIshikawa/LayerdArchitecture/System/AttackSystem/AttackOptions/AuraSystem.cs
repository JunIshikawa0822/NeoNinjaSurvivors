using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSystem : AttackOptionBase, IOnUpdate
{
    public override void AttackOptionSetUp()
    {
        gameStat.auraObjectData.InitializeAuraLevels();
        gameStat.auraObjectData.SetLevel(gameStat.auraSkillLevel);
    }

    public void OnUpdate()
    {
        if (gameStat.isAuraUsing && gameStat.activeAuraInstance == null) {
            AuraActivation(gameStat.aura,ref gameStat.activeAuraInstance, gameStat.player.transform.position);
        } else if (!gameStat.isAuraUsing && gameStat.activeAuraInstance != null) {
            AuraDeActivation(ref gameStat.activeAuraInstance);
        } else if(gameStat.isAuraUsing && gameStat.activeAuraInstance != null) {
            AuraScale(ref gameStat.activeAuraInstance,gameStat.auraObjectData.AuraRadius, gameStat.player.transform.position);
            AuraProcess(gameStat.activeAuraInstance, gameStat.auraObjectData.AuraRadius, gameStat.auraObjectData.AuraNockBackStrength, gameStat.auraObjectData.AuraNockBackInterval, gameStat.auraObjectData.AuraDamage, gameStat.enemyTimers);
        }

        
    }

    private void AuraScale(ref Aura _activeAuraInstance, float _auraRadius, Vector3 _playerPos){
        _activeAuraInstance.transform.localScale = new Vector3(_auraRadius, 1, _auraRadius);
        _activeAuraInstance.transform.position = _playerPos;
    }

    private void AuraProcess(Aura _activeAuraInstance, float _auraRadius, float _auraNBStrength, float _auraNBInterval, int _auraDamage, Dictionary<EnemyBase,float> _enemyTimers){
        // 指定された半径内のコライダーを検出
        Collider[] hitColliders = Physics.OverlapSphere(_activeAuraInstance.transform.position, _auraRadius/2);
        
        // 検出したコライダーに対して処理を行う
        foreach (var hitCollider in hitColliders)
        {
            if(!hitCollider.gameObject.CompareTag("Enemy")) continue;
            
            EnemyBase enemy = hitCollider.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                if (!_enemyTimers.ContainsKey(enemy))
                {
                    _enemyTimers[enemy] = Time.time + _auraNBInterval;
                }

                if (Time.time >= _enemyTimers[enemy])
                {
                    enemy.EnemyGetDamage(_auraDamage,_activeAuraInstance.transform.position, enemy.transform.position, _auraNBStrength);
                    _enemyTimers[enemy] = Time.time + _auraNBInterval;
                }
            }
        }
        CleanUpEnemies(_enemyTimers);
    }

    private void CleanUpEnemies(Dictionary<EnemyBase,float> _enemyTimers)
    {
        List<EnemyBase> enemiesToRemove = new List<EnemyBase>();
        foreach (var entry in _enemyTimers)
        {
            if (entry.Key == null)
            {
                enemiesToRemove.Add(entry.Key);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            _enemyTimers.Remove(enemy);
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
