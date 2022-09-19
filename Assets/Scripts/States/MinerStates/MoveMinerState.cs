using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class MoveMinerState : MinerBaseState
    {
        public override void EnterState(MinerManager minerManager)
        {
            minerManager.agent.SetDestination(minerManager.GemVeins.transform.position);
        }
        

        public override void OnTriggerEnter(MinerManager minerManager,Collider other)
        {
            if (other.CompareTag("Mine"))
            {
                Debug.Log("Varmişem");
                minerManager.SwitchState(minerManager.DigMiner);
            }
        }
        
    }
}