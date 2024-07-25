using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderObjectData", menuName = "ScriptableObject/ThunderObjectData", order = 0)]
public class ThunderObjectData : ScriptableObject
{
    [System.Serializable]
    public class ThunderParameters
    {
        public float thunderRange;
        public float thunderInterval;
        public int thunderDamage;
        public float thunderNockBackStrength;

        public ThunderParameters(float _range, float _interval, int _damage, float _nockBackStrength)
        {
            thunderRange = _range;
            thunderInterval = _interval;
            thunderDamage = _damage;
            thunderNockBackStrength = _nockBackStrength;
        }
    }

    [SerializeField] private List<ThunderParameters> thunderLevels = new List<ThunderParameters>();

    [System.NonSerialized] public float thunderTime = 0f;

    private ThunderParameters currentParameters;

    public void InitializeThunderLevels()
    {
        thunderLevels.Clear();
        thunderLevels.Add(new ThunderParameters(10f, 5f, 5, 5f));
        thunderLevels.Add(new ThunderParameters(12f, 4.5f, 6, 6f));
        thunderLevels.Add(new ThunderParameters(14f, 4f, 7, 7f));
        thunderLevels.Add(new ThunderParameters(16f, 3.5f, 8, 8f));
        thunderLevels.Add(new ThunderParameters(18f, 3f, 9, 9f));
        thunderLevels.Add(new ThunderParameters(20f, 2.5f, 10, 10f));
        thunderLevels.Add(new ThunderParameters(22f, 2f, 11, 11f));
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > thunderLevels.Count)
        {
            Debug.LogWarning("異様なレベル検知です: " + level);
            return;
        }
        
        currentParameters = thunderLevels[level - 1];
        Debug.Log("サンダーレベル更新 : " + level);
    }

    public float ThunderRange => currentParameters?.thunderRange ?? 0f;
    public float ThunderInterval => currentParameters?.thunderInterval ?? 0f;
    public int ThunderDamage => currentParameters?.thunderDamage ?? 0;
    public float ThunderNockBackStrength => currentParameters?.thunderNockBackStrength ?? 0f;
}
