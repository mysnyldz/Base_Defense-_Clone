using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData
    {
        public int TurretPrice;
        public int PayedValue;
        public int TurretId;
        public RoomStageType roomStageType = RoomStageType.Uncomplete;
    }
}