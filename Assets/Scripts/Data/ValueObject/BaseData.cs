using System;
using System.Collections.Generic;
using Abstract;
using Enums;
using UnityEngine;
using ValueObject;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseData : SaveableEntity
    {
        public string Key = "IdleBaseDataKey";

        public bool IsCompleted;

        public Turret Turret;

        public int RoomPRice;

        public int PayedValue;

        public RoomStageType roomStageType;
    }
    public BaseData
    public override string GetKey()
    {
        return Key;
    }
}