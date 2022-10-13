using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomTurretData
    {
        public int TurretPrice;
        public int PayedValue;
        public int TurretId;
        public RoomStageTypes roomStageTypes = RoomStageTypes.UnComplete;
    }
}