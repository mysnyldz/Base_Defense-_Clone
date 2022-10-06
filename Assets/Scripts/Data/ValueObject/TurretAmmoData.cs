using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretAmmoData
    {
        #region GemDepotData

        public int MaxAmmoCapacity;
        public Vector3 AmmoInitPoint;
        [Range(0, 10)] public int AmmoCountX;
        [Range(0, 10)] public int AmmoCountZ;
        [Range(0, 10)] public int AmmoCountY;
        [Range(0, 5)] public float OffsetFactorX;
        [Range(0, 5)] public float OffsetFactorY;
        [Range(0, 5)] public float OffsetFactorZ;
        

        #endregion
    }
}