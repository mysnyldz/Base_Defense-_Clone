using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Structure", menuName = "CD_Objects/CD_Structure", order = 0)]
    public class CD_Structure : ScriptableObject
    {
        public RoomData Data;
    }
}