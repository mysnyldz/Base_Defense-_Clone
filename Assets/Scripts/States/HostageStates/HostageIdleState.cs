using Abstract;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace States.HostageStates
{
    public class HostageIdleState : HostageBaseState
    {
        #region Self Variables

        #region Private Variables

        private HostageManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public HostageIdleState(ref HostageManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public override void EnterState()
        {
            _manager.Player = null;
            _manager.SetTriggerAnimation(HostageAnimTypes.Scared);
        }

        public override void UpdateState()
        {
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Player = other.transform.parent.gameObject;
                _manager.SwitchState(HostageStatesTypes.Follow);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}