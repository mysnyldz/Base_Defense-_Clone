using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MineZoneData", menuName = "CD_Objects/CD_MineZoneData", order = 0)]
    public class CD_MineZoneData : ScriptableObject
    {
        public MineZoneData Data;
    }
}