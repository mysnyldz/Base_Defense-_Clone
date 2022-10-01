using Abstract;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace States.MinerStates
{
    public class MoveDepotState : MinerBaseState
    {
        public override void EnterState(MinerManager minerManager)
        {
            minerManager.agent.SetDestination(minerManager.GemDepot.transform.position);
            minerManager.SetTriggerAnim(MinerAnimTypes.Carry);
        }

       
        public override void OnTriggerEnter(MinerManager minerManager, Collider other)
        {
            if (other.CompareTag("GemDepot"))
            {
                IdleSignals.Instance.onDepotAddGem?.Invoke(minerManager.transform.gameObject);
                minerManager.SwitchState(MinerStatesType.MoveMine);
            }
        }

        
    }
}