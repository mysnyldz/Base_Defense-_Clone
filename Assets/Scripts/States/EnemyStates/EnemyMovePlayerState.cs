using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMovePlayerState: EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyTypesData _data;
        #endregion

        #endregion

        public EnemyMovePlayerState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyTypesData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }
        public override void EnterState()
        {
            _agent.SetDestination(_manager.Player.transform.position);
            _agent.speed = _data.RunSpeed;
            _manager.SetTriggerAnim(EnemyAnimTypes.Run);
            
        }

        public override void UpdateState()
        {
            _agent.destination = _manager.Player.transform.position;
            if (_data.AttackRange > _agent.remainingDistance)
            {
                _manager.SwitchState(EnemyStatesTypes.Attack);
            }

            if (_manager.Health())
            {
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.SwitchState(EnemyStatesTypes.Attack);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
            }
        }
    }
}