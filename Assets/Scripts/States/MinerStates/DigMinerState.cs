using System.Threading.Tasks;
using Abstract;
using Cinemachine;
using Enums;
using Managers;
using UnityEngine;

namespace States.MinerStates
{
    public class DigMinerState : MinerBaseState
    {
        public override void EnterState(MinerManager minerManager)
        {
            Timer(minerManager);
        }

        public override void OnTriggerEnter(MinerManager minerManager, Collider other)
        {
        }

        private async void Timer(MinerManager minerManager)
        {
            await Task.Delay(2750);
            minerManager.SwitchState(MinerStatesType.MoveDepot);
        }
        
    }
}