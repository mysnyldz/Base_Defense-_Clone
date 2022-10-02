using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomData
    {
        public List<RoomIDData> RoomIDList = new List<RoomIDData>();
    }
}