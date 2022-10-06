using System;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyTypesData
    {
        public int Health;
        public float MoveSpeed;
        public float RunSpeed;
        public int Damage;
        public float AttackRange;
    }
}