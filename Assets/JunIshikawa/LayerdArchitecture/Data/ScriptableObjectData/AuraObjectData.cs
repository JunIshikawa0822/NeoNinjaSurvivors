using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AuraData", menuName = "ScriptableObject/AuraData", order = 0)]
public class AuraObjectData : ScriptableObject
{
    [System.Serializable]
    public class AuraParameters
    {
        public float auraRadius;
        public float auraNockBackInterval;
        public float auraNockBackStrength;
        public int auraDamage;

        public AuraParameters(float radius, float nockBackInterval, float nockBackStrength, int damage)
        {
            auraRadius = radius;
            auraNockBackInterval = nockBackInterval;
            auraNockBackStrength = nockBackStrength;
            auraDamage = damage;
        }
    }

    [SerializeField] private Dictionary<int, AuraParameters> auraLevels = new Dictionary<int, AuraParameters>();

    [System.NonSerialized] public float elapsedAuraTime = 0f;

    private AuraParameters currentParameters;

    public void InitializeAuraLevels()
    {
        // レベルごとの設定をDictionaryに追加
        auraLevels.Add(1, new AuraParameters(5f, 3f, 5f, 1));
        auraLevels.Add(2, new AuraParameters(6f, 2.5f, 6f, 2));
        auraLevels.Add(3, new AuraParameters(7f, 2f, 7f, 3));
        auraLevels.Add(4, new AuraParameters(7f, 2f, 7f, 3));
        auraLevels.Add(5, new AuraParameters(7f, 2f, 7f, 3));
        auraLevels.Add(6, new AuraParameters(7f, 2f, 7f, 3));
        auraLevels.Add(7, new AuraParameters(7f, 2f, 7f, 3));
    }

    public void SetLevel(int level)
    {
        if (auraLevels.TryGetValue(level, out currentParameters))
        {
            Debug.Log("オーラレベル更新 : "+ level);
        }
        else
        {
            Debug.LogWarning("異様なレベル検知です");
        }
    }

    public float AuraRadius => currentParameters.auraRadius;
    public float AuraNockBackInterval => currentParameters.auraNockBackInterval;
    public float AuraNockBackStrength => currentParameters.auraNockBackStrength;
    public int AuraDamage => currentParameters.auraDamage;
}
