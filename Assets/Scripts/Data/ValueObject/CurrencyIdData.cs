using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class CurrencyIdData : SaveableEntity
    {
        public static string CurrencyKey = "Currency";

        public float Money;
        public float Gem;

        public CurrencyIdData(float _money, float _gem)
        {
            Money = _money;
            Gem = _gem;
        }

        public CurrencyIdData()
        {
            
        }

        public override string GetKey()
        {
            return CurrencyKey;
        }
    }
}