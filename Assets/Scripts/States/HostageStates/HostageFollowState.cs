using Abstract;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.HostageStates
{
    public class HostageFollowState : HostageBaseState
    {
        #region Self Variables

        #region Private Variables

        private HostageManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public HostageFollowState(ref HostageManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public override void EnterState()
        {
            IdleSignals.Instance.onRemoveHostageSpawnPoint.Invoke(_manager.gameObject);
            _manager.SwitchState(HostageStatesTypes.Follow);
        }

        public override void UpdateState()
        {
            _agent.SetDestination(_manager.Player.transform.position);
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MinerTent"))
            {
                _manager.Tent = other.gameObject;
                _manager.SwitchState(HostageStatesTypes.Tent);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}