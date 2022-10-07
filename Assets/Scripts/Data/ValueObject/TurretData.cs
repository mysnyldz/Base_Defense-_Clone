using System;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData
    {
        public TurretDepotAmmoData DepotAmmoData;
        public int FireRate;
        public int Damage;

    }
}