using Abstract;
using Signals;
using UnityEngine;

namespace States.MinerStates
{
    public class MoveDepotState : MinerBaseState
    {
        public override void EnterState(MinerManager minerManager)
        {
            minerManager.agent.SetDestination(minerManager.GemDepot.transform.position);
        }

       
        public override void OnTriggerEnter(MinerManager minerManager, Collider other)
        {
            if (other.CompareTag("GemDepot"))
            {
                Debug.Log("Depoliyem");
                IdleSignals.Instance.onDepotAddGem?.Invoke(minerManager.transform);
                minerManager.SwitchState(minerManager.MoveMiner);
            }
        }

        
    }
}