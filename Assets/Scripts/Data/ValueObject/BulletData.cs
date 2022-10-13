using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class BulletData
    {
        public SerializedDictionary<BulletTypes, BulletTypesData> BulletTypeDatas;
    }
}