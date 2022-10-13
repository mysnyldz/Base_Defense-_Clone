using UnityEngine;

namespace Abstract
{
    public abstract class EnemyBaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerExit(Collider other);
    }
}