using Abstract;

namespace Data.ValueObject
{
    public class CurrencyIdData : SaveableEntity
    {
        public static string CurrencyKey = "Currency";

        public float MoneyId;
        public float GemId;

        public CurrencyIdData(float _moneyId, float _gemId)
        {
            MoneyId = _moneyId;
            GemId = _gemId;
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