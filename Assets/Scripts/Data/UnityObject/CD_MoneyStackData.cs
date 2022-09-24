using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MoneyStackData", menuName = "CD_Objects/CD_MoneyStackData", order = 0)]
    public class CD_MoneyStackData : ScriptableObject
    {
        public MoneyStackData Data;

    }
}