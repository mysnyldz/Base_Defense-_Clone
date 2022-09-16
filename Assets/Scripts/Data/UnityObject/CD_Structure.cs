using System.Collections.Generic;
using UnityEngine;
using ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Structure", menuName = "CD_Objects/CD_Structure", order = 0)]
    public class CD_Structure : ScriptableObject
    {
        public List<RoomData> RoomData;
    }
}