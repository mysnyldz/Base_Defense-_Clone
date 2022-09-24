using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "CD_Objects/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public List<PoolValueData> PoolValueDatas;
    }
}