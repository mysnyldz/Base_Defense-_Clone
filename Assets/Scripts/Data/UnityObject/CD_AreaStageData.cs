using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AreaStageData", menuName = "CD_Objects/CD_AreaStageData", order = 0)]
    public class CD_AreaStageData : ScriptableObject
    {
        public AreaStageData Data;
    }
}