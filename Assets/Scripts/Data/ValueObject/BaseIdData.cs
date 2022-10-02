using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseIdData : SaveableEntity
    {
        public static string BaseKey = "Base";

        public int BaseId;

        
        public BaseIdData(int _baseId)
        {
            BaseId = _baseId;
        }
        public BaseIdData()
        {
            
        }

        public override string GetKey()
        {
            return BaseKey;
        }
    }
}