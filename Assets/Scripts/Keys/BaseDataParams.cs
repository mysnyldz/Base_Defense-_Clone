using System;
using System.Collections.Generic;
using ValueObject;

namespace Keys
{
    [Serializable]
    public struct BaseDataParams
    {
        public int BaseLevel;
        public Dictionary<int, RoomData> RoomDictionary;
    }
}