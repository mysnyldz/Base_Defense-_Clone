using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData
    {
        public SerializedDictionary<EnemyTypes, EnemyTypesData> EnemyTypeDatas;
    }
}