using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class MoneyStackData
    {
        public Vector3 MoneyInitPoint;
        public int PlayerMaxMoneyCount;
        [Range(0, 10)] public int MoneyCountX;
        [Range(0, 25)] public int MoneyCountY;
        [Range(0, 10)] public int MoneyCountZ;
        [Range(0, 5)] public float OffsetFactorX;
        [Range(0, 5)] public float OffsetFactorY;
        [Range(0, 5)] public float OffsetFactorZ;

        [Header("Supporter"),Space(10)]public int SupporterMaxMoneyCount;
    }
}