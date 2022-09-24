using System.Threading.Tasks;
using Abstract;
using Cinemachine;
using Enums;
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
            Debug.Log("Kaziyem");
            await Task.Delay(2750);
            minerManager.SwitchState(MinerStatesType.MoveDepot);
        }
        
    }
}