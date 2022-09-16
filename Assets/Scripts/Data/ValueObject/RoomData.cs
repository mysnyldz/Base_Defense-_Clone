using System;
using Abstract;
using Data.ValueObject;
using Enums;

namespace ValueObject
{
    [Serializable]
    public class RoomData : SaveableEntity
    {
        public string Key = "IdleRoomDataKey";

        public bool IsCompleted;

        public TurretData Turret;

        public int RoomPrice;

        public int PayedAmount;

        public int RoomID;

        public RoomStageTypes RoomStageType;

        public RoomData(bool isCompleted, TurretData turret, int roomId, int roomPrice, int payedAmount,
            RoomStageTypes roomStageType)
        {
            IsCompleted = isCompleted;
            Turret = turret;
            RoomID = roomId;
            RoomPrice = roomPrice;
            PayedAmount = payedAmount;
            RoomStageType = roomStageType;
        }

        public override string GetKey()
        {
            return Key;
        }
    }
}