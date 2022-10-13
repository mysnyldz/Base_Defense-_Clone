using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class CurrencyIdData : SaveableEntity
    {
        public static string CurrencyKey = "Currency";
        
        public int Money;
        public int Gem;

        public CurrencyIdData(int money, int gem)
        {
            Money = money;
            Gem = gem;
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