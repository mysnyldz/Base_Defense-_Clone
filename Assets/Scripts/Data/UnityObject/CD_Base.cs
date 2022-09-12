using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Base", menuName = "CD_Objects/CD_Base", order = 0)]
    public class CD_Base : ScriptableObject
    {
        public List<BaseData> Bases = new List<BaseData>();
    }
}