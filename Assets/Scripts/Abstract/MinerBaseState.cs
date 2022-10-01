using Managers;
using UnityEngine;

namespace Abstract
{
    public abstract class MinerBaseState
    {
       public abstract void EnterState(MinerManager minerManager);
       public abstract void OnTriggerEnter(MinerManager minerManager, Collider other);
    }
}