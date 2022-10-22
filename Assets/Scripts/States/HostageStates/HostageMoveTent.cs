using Abstract;
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
            _manager.SwitchState(HostageStatesTypes.Tent);
            _agent.SetDestination(_manager.Tent.transform.position);
        }

        public override void UpdateState()
        {
            if (_agent.transform.position == _manager.Tent.transform.position)
            {
                PoolSignals.Instance.onReleasePoolObject.Invoke(PoolType.Hostage.ToString(), _manager.gameObject);
                
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}