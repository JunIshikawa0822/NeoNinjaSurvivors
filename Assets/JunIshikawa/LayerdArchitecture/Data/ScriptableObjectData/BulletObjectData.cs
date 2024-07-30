using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/BulletData", order = 0)]
public class BulletObjectData : ScriptableObject
{
    [System.Serializable]
    public class BulletParameters
    {
        public float bulletSpeed;
        public float maxDistance;
        public int bulletDamage;
        public int penetrateCount;
        public int simulNumLevel;
        public int angleLevel;
        public float coolTime;

        public BulletParameters(float speed, float distance, int damage, int penetrate, int simulNum, int angle, float _coolTime)
        {
            bulletSpeed = speed;
            maxDistance = distance;
            bulletDamage = damage;
            penetrateCount = penetrate;
            simulNumLevel = simulNum;
            angleLevel = angle;
            coolTime = _coolTime;
        }
    }

    [SerializeField] private List<BulletParameters> bulletLevels = new List<BulletParameters>();

    [System.NonSerialized] public int[] bulletAngleLevelArray = new int[] { 0, 5, 7, 10, 12, 15, 17, 20 };

    private BulletParameters currentParameters;

    public void InitializeBulletLevels()
    {
        bulletLevels.Clear();
        bulletLevels.Add(new BulletParameters(0.01f, 100f, 1, 1, 1, 1, 2f));
        bulletLevels.Add(new BulletParameters(0.01f, 120f, 2, 2, 2, 2, 1.7f));
        bulletLevels.Add(new BulletParameters(0.01f, 150f, 3, 3, 3, 3, 1.5f));
        bulletLevels.Add(new BulletParameters(0.01f, 180f, 4, 4, 4, 4, 1f));
        bulletLevels.Add(new BulletParameters(0.01f, 200f, 5, 5, 5, 5, 0.8f));
        bulletLevels.Add(new BulletParameters(0.01f, 220f, 6, 6, 6, 6, 0.7f));
        bulletLevels.Add(new BulletParameters(0.01f, 250f, 7, 7, 7, 7, 0.6f));
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > bulletLevels.Count)
        {
            Debug.LogWarning("異様なレベル検知です: " + level);
            return;
        }

        currentParameters = bulletLevels[level - 1];
        Debug.Log("バレットレベル更新 : " + level);
    }

    public float BulletSpeed => currentParameters?.bulletSpeed ?? 0f;
    public float MaxDistance => currentParameters?.maxDistance ?? 0f;
    public int BulletDamage => currentParameters?.bulletDamage ?? 0;
    public int PenetrateCount => currentParameters?.penetrateCount ?? 1;
    public int SimulNumLevel => currentParameters?.simulNumLevel ?? 1;
    public int AngleLevel => currentParameters?.angleLevel ?? 1;
    public float CoolTime => currentParameters?.coolTime ?? 0;
}
