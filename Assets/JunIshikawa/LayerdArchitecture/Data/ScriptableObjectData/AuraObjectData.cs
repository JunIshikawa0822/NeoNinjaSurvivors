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

    [SerializeField] private List<AuraParameters> auraLevels = new List<AuraParameters>();

    [System.NonSerialized] public float elapsedAuraTime = 0f;

    private AuraParameters currentParameters;

    public void InitializeAuraLevels()
    {
        auraLevels.Clear();
        auraLevels.Add(new AuraParameters(5f, 3f, 5f, 1));
        auraLevels.Add(new AuraParameters(6f, 2.5f, 6f, 2));
        auraLevels.Add(new AuraParameters(7f, 2f, 7f, 3));
        auraLevels.Add(new AuraParameters(8f, 1.5f, 8f, 4));
        auraLevels.Add(new AuraParameters(9f, 1f, 9f, 5));
        auraLevels.Add(new AuraParameters(10f, 0.5f, 10f, 6));
        auraLevels.Add(new AuraParameters(11f, 0.25f, 11f, 7));
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > auraLevels.Count)
        {
            Debug.LogWarning("異様なレベル検知です: " + level);
            return;
        }

        currentParameters = auraLevels[level - 1];
        Debug.Log("オーラレベル更新 : " + level);
    }

    public float AuraRadius => currentParameters?.auraRadius ?? 0f;
    public float AuraNockBackInterval => currentParameters?.auraNockBackInterval ?? 0f;
    public float AuraNockBackStrength => currentParameters?.auraNockBackStrength ?? 0f;
    public int AuraDamage => currentParameters?.auraDamage ?? 0;
}
