using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseData
    {
        [Header("Data"), Space(10)] public int BaseCount;
    }
}