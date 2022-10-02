using System;
using System.Collections.Generic;
using Abstract;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseData
    {
        public List<BaseIdData> BaseIdDatas = new List<BaseIdData>();
    }
    
}