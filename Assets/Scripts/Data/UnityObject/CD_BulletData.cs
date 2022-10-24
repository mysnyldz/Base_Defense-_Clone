using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BulletData", menuName = "CD_Objects/CD_BulletData", order = 0)]
    public class CD_BulletData : ScriptableObject
    {
        public BulletData Data;
    }
}