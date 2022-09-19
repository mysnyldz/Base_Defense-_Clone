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
        [Range(1, 5)] public float OffsetFactor;

        #endregion

        public int MaxMinerCapacity;
        public int CurrentMinerAmount;
        public int MineCartCapacity;
    }
}