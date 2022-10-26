using System.Threading.Tasks;
using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.HostageStates
{
    public class HostageMoveTent : HostageBaseState
    {
        #region Self Variables

        #region Private Variables

        private HostageManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public HostageMoveTent(ref HostageManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }


        public override void EnterState()
        {
            TentController();
        }

        public override void UpdateState()
        {
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }

        private void TentController()
        {
            if (_manager.CurrentMinerAmount < _manager.MaxMinerCount)
            {
                _manager.HostageController.TurnToMiner();
            }
            else
            {
                _manager.SwitchState(HostageStatesTypes.Follow);
            }
        }
    }
}