using Managers;
using UnityEngine;

namespace Abstract
{
    public abstract class EnemyBaseState
    {
        public abstract void EnterState(EnemyManager minerManager);
        public abstract void OnTriggerEnter(EnemyManager minerManager, Collider other);
    }
}