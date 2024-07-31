using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShurikenData", menuName = "ScriptableObject/ShurikenData", order = 0)]
public class ShurikenObjectData : ScriptableObject
{
    [System.Serializable]
    public class ShurikenParameters
    {
        public float shurikenSpeed;
        public float maxDistance;
        public int shurikenDamage;
        public int simulNumLevel;
        public int angleLevel;
        public float coolTime;

        public ShurikenParameters(float speed, float distance, int damage, int simulNum, int angle, float _coolTime)
        {
            shurikenSpeed = speed;
            maxDistance = distance;
            shurikenDamage = damage;
            simulNumLevel = simulNum;
            angleLevel = angle;
            coolTime = _coolTime;
        }
    }

    [SerializeField] private List<ShurikenParameters> shurikenLevels = new List<ShurikenParameters>();

    [System.NonSerialized] public int[] shurikenAngleLevelArray = new int[] { 0, 5, 7, 10, 12, 15, 17, 20 };

    private ShurikenParameters currentParameters;

    public void InitializeShurikenLevels()
    {
        shurikenLevels.Clear();
        shurikenLevels.Add(new ShurikenParameters(0.025f, 100f, 2, 1, 1, 5f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 120f, 2, 1, 2, 4.5f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 150f, 5, 2, 3, 4f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 180f, 5, 2, 4, 3f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 200f, 7, 2, 5, 3.5f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 220f, 7, 3, 6, 3f));
        shurikenLevels.Add(new ShurikenParameters(0.025f, 250f, 10, 3, 7, 2.5f));
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > shurikenLevels.Count)
        {
            Debug.LogWarning("異様なレベル検知です: " + level);
            return;
        }

        currentParameters = shurikenLevels[level - 1];
        Debug.Log("バレットレベル更新 : " + level);
    }

    public float BulletSpeed => currentParameters?.shurikenSpeed ?? 0f;
    public float MaxDistance => currentParameters?.maxDistance ?? 0f;
    public int BulletDamage => currentParameters?.shurikenDamage ?? 0;
    public int SimulNumLevel => currentParameters?.simulNumLevel ?? 1;
    public int AngleLevel => currentParameters?.angleLevel ?? 1;
    public float CoolTime => currentParameters?.coolTime ?? 0;
}