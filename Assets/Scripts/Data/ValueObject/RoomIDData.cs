using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomIDData : SaveableEntity
    {
        public static string Key = "RoomKey";

        //public RoomTypes RoomTypes;
        
        public RoomTurretData roomTurret;

        public int RoomPrice;

        public int PayedAmount;

        public int RoomID;
        
        public RoomStageTypes RoomStageType;

        public RoomIDData(RoomTypes roomTypes ,RoomTurretData roomTurret, int roomId, int roomPrice, int payedAmount,
            RoomStageTypes roomStageType)
        {
           // RoomTypes = roomTypes;
            this.roomTurret = roomTurret;
            RoomID = roomId;
            RoomPrice = roomPrice;
            PayedAmount = payedAmount;
            RoomStageType = roomStageType;
        }

        public RoomIDData()
        {
            
        }
        

        public override string GetKey()
        {
            return Key;
        }
    }
}