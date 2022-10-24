using System;
using System.Collections.Generic;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class AreaStageData
    {
        public List<AreaData> AreaData = new List<AreaData>();
    }
}