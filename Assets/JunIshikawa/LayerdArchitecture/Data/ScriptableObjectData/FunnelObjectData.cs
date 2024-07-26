using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FunnelData", menuName = "ScriptableObject/FunnelData", order = 0)]
public class FunnelObjectData : ScriptableObject
{
    [SerializeField]
    public float radius;

    [SerializeField]
    public float theta;

    [SerializeField]
    public float moveMaxDistance;

    [SerializeField]
    public float speed;

    [System.Serializable]
    public class FunnelParameters
    {
        public int funnelDamage;
        public int simulNumLevel;
        public float coolTime;

        public FunnelParameters(int _damage, int _simulNum, float _coolTime)
        {
            funnelDamage = _damage;
            simulNumLevel = _simulNum;
            coolTime = _coolTime;
        }
    }

    [SerializeField] private List<FunnelParameters> funnelLevels = new List<FunnelParameters>();

    private FunnelParameters currentParameters;

    public void InitializeBulletLevels()
    {
        funnelLevels.Clear();
        funnelLevels.Add(new FunnelParameters(3, 1, 6));
        funnelLevels.Add(new FunnelParameters(3, 2, 6));
        funnelLevels.Add(new FunnelParameters(4, 3, 5));
        funnelLevels.Add(new FunnelParameters(4, 4, 5));
        funnelLevels.Add(new FunnelParameters(5, 5, 4));
        funnelLevels.Add(new FunnelParameters(5, 6, 4));
        funnelLevels.Add(new FunnelParameters(6, 7, 3));
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > funnelLevels.Count)
        {
            Debug.LogWarning("異様なレベル検知です: " + level);
            return;
        }

        currentParameters = funnelLevels[level - 1];
        Debug.Log("ファンネルレベル更新 : " + level);
    }

    public int FunnelDamage => currentParameters?.funnelDamage ?? 1;
    public int SimulNumLevel => currentParameters?.simulNumLevel ?? 1;
    public float CoolTime => currentParameters?.coolTime ?? 3;
}
