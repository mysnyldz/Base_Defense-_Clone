using Abstract;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class MoveMinerState : MinerBaseState
    {
        public override void EnterState(MinerManager minerManager)
        {
            minerManager.agent.SetDestination(minerManager.GemVeins.transform.position);
            minerManager.SetTriggerAnim(MinerAnimTypes.Run);
        }
        

        public override void OnTriggerEnter(MinerManager minerManager,Collider other)
        {
            if (other.CompareTag("Mine"))
            {
                minerManager.SetTriggerAnim(MinerAnimTypes.Dig); 
                minerManager.SwitchState(MinerStatesType.Dig);
            }
            else if (other.CompareTag("MineCart"))
            {
                minerManager.SetTriggerAnim(MinerAnimTypes.Gather); 
                minerManager.SwitchState(MinerStatesType.Gather);
            }
            
        }
        
    }
}