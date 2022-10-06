using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyDeathState: EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyTypesData _data;
        #endregion

        #endregion

        public EnemyDeathState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyTypesData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }
        public override void EnterState()
        {
            _agent.enabled = false;
            _agent.gameObject.SetActive(false);
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
    }
}