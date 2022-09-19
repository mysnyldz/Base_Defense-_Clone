using System.Threading.Tasks;
using Abstract;
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
            Debug.Log("Depoyagidiyem");
            await Task.Delay(2000);
            minerManager.SwitchState(minerManager.MoveDepot);
        }
        
    }
}