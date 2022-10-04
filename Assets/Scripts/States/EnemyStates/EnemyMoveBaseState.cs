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
            _agent.speed = _data.MoveSpeed;
            _manager.SetTriggerAnim(EnemyAnimTypes.Walk);
            _agent.SetDestination(_manager.BasePoints.transform.position);
        }

        public override void UpdateState()
        {
            if (_manager.Health())
            {
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBasePoints"))
            {
                _manager.SwitchState(EnemyStatesTypes.Attack);
            }

            if (other.CompareTag("PlayerSphere"))
            {
                _manager.Player = other.transform.parent.gameObject;
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            
        }
    }
}