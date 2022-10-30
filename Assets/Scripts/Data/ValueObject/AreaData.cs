using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class AreaData
    {
        public int Price;
        public int PayedPrice;
        public AreaType AreaType;
        public AreaStageType AreaStageType;
    }
}