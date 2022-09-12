using Data.ValueObject.InputData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "CD_Objects/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData InputData;
    }
    
}