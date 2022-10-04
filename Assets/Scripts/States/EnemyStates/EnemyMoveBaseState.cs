using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMoveBaseState: EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyTypesData _data;
        private EnemyBaseState _enemyBaseStateImplementation;
        private EnemyBaseState _enemyBaseStateImplementation1;

        #endregion

        #endregion

        public EnemyMoveBaseState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyTypesData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }
        public override void EnterState()
        {
            _agent.SetDestination(_manager.BasePoints.transform.position);
            _manager.SetTriggerAnim(EnemyAnimTypes.Walk);
        }

        public override void UpdateState()
        {
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBasePoint"))
            {
                _manager.SwitchState(EnemyStatesTypes.Attack);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            
        }
    }
}