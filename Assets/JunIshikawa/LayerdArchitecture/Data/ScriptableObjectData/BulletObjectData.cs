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

        public BulletParameters(float speed, float distance, int damage, int penetrate, int simulNum, int angle)
        {
            bulletSpeed = speed;
            maxDistance = distance;
            bulletDamage = damage;
            penetrateCount = penetrate;
            simulNumLevel = simulNum;
            angleLevel = angle;
        }
    }

    [SerializeField] private Dictionary<int, BulletParameters> bulletLevels = new Dictionary<int, BulletParameters>();

    [System.NonSerialized] public int[] bulletAngleLevelArray = new int[] { 0, 5, 7, 10, 12, 15, 17, 20 };

    private BulletParameters currentParameters;

    public void InitializeBulletLevels()
    {
        bulletLevels.Add(1, new BulletParameters(0.1f, 100f, 1, 1, 1, 1));
        bulletLevels.Add(2, new BulletParameters(1.5f, 100f, 2, 2, 2, 2));
        bulletLevels.Add(3, new BulletParameters(2.0f, 100f, 3, 3, 3, 3));
        bulletLevels.Add(4, new BulletParameters(1.3f, 100f, 4, 4, 4, 4));
        bulletLevels.Add(5, new BulletParameters(1.4f, 100f, 5, 5, 5, 5));
        bulletLevels.Add(6, new BulletParameters(8f, 100f, 6, 6, 6, 6));
        bulletLevels.Add(7, new BulletParameters(9f, 100f, 7, 7, 7, 7));
    }

    public void SetLevel(int level)
    {
        if (bulletLevels.TryGetValue(level, out currentParameters))
        {
            Debug.Log("バレットレベル更新 : "+ level);
        }
        else
        {
            Debug.LogWarning("異様なレベル検知です");
        }
    }

    public float BulletSpeed => currentParameters?.bulletSpeed ?? 0f;
    public float MaxDistance => currentParameters?.maxDistance ?? 0f;
    public int BulletDamage => currentParameters?.bulletDamage ?? 0;
    public int PenetrateCount => currentParameters?.penetrateCount ?? 1;
    public int SimulNumLevel => currentParameters?.simulNumLevel ?? 1;
    public int AngleLevel => currentParameters?.angleLevel ?? 1;
}
