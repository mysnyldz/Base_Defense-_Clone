using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class MineZoneData
    {
        #region GemDepotData

        public int MaxGemCapacity;
        public Vector3 GemInitPoint;
        [Range(1, 10)] public int GemCountX;
        [Range(1, 10)] public int GemCountZ;
        [Range(1, 5)] public float OffsetFactorX;
        [Range(1, 5)] public float OffsetFactorY;
        [Range(1, 7)] public float OffsetFactorZ;
        

        #endregion

        public int MaxMinerCapacity;
        public int CurrentMinerAmount;
        public int MineCartCapacity;
    }
}