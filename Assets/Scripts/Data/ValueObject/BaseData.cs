using System;
using System.Collections.Generic;
using Abstract;
using Enums;
using UnityEngine;
using ValueObject;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseData
    {
        public List<BaseLevel> BaseLevelList = new List<BaseLevel>();
        public MineZoneData mineZoneData;


    }
    
}