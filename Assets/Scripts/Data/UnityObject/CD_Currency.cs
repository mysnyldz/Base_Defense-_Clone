using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Currency", menuName = "CD_Objects/CD_Currency", order = 0)]
    public class CD_Currency : ScriptableObject
    {
        public CurrencyIdData currencyIdData;
    }
}