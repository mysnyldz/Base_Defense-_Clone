using System;
using Abstract;
using Data.ValueObject;
using Enums;

namespace ValueObject
{
    [Serializable]
    public class RoomData: SaveableEntity
    {
        public string Key = "IdleRoomDataKey";

        public bool IsCompleted;

        public TurretData Turret;

        public int RoomPRice;

        public int PayedValue;

        public int RoomID;

        public RoomStageType roomStageType;
    }
    public RoomData(){}

    public RoomData(bool isCompleted, TurretData turret, int roomId, int roomPrice, int payedAmount,
        RoomStageType roomStageType)
    {
        IsCompleted = isCompleted;
        Turret = turret;
        RoomID = roomId;
        


    }
    public override string GetKey()
    {
        return Key;
    }
}