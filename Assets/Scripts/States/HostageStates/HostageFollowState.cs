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
            _manager.SetTriggerAnimation(HostageAnimTypes.Idle);
            IdleSignals.Instance.onRemoveHostageSpawnPoint.Invoke(_manager.gameObject);
        }

        public override void UpdateState()
        {
            Follow();
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

        private void Follow()
        {
            _agent.SetDestination(_manager.Player.transform.position);
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _manager.SetBoolAnimation(HostageAnimTypes.Walk,false);
                return;
            }
            _manager.SetBoolAnimation(HostageAnimTypes.Walk,true);
        }
    }
}