using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AmmoStackData", menuName = "CD_Objects/CD_AmmoStackData", order = 0)]
    public class CD_AmmoStackData : ScriptableObject
    {
        public AmmoStackData Data;
    }
}