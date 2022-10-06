using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_TurretAmmoData", menuName = "CD_Objects/CD_TurretAmmoData", order = 0)]
    public class CD_TurretAmmoData : ScriptableObject
    {
        public TurretAmmoData Data;
    }
}