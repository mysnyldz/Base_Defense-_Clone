using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretDepotAmmoData
    {
        #region Capacity & Startpos

        [Space(10), Header("Capacity & Startpos")]
        public int MaxAmmoCapacity;
        public Vector3 AmmoInitPoint;
        
        #endregion

        #region Count
        [Space(10), Header("Counts")]
        [Range(0, 10)] public int AmmoCountX;
        [Range(0, 10)] public int AmmoCountZ;
        [Range(0, 10)] public int AmmoCountY;

        #endregion

        #region Offsets
        [Space(10), Header("Offsets")] 
        [Range(0, 5)] public float OffsetFactorX;
        [Range(0, 5)] public float OffsetFactorY;
        [Range(0, 5)] public float OffsetFactorZ;

        #endregion
    }
}