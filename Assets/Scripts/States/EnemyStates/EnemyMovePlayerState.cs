using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMovePlayerState : EnemyBaseState
    {
        #region Self Variables

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyData _data;
        private EnemyTypes _types;

        #endregion

        #endregion

        public EnemyMovePlayerState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data,
            ref EnemyTypes enemyTypes)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = enemyTypes;
        }

        public override void EnterState()
        {
            _agent.SetDestination(_manager.Player.transform.position);
            _agent.speed = _data.EnemyTypeDatas[_types].RunSpeed;
            _manager.SetTriggerAnim(EnemyAnimTypes.Run);
        }

        public override void UpdateState()
        {
            _agent.destination = _manager.Player.transform.position;
            if ((_manager.transform.position-_manager.Player.transform.position).sqrMagnitude < Mathf.Pow(_data.EnemyTypeDatas[_types].AttackRange,2))
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
            if (other.CompareTag("MineTargetSphere"))
            {
                _manager.MineTnt = other.transform.parent.gameObject;
                _manager.SwitchState(EnemyStatesTypes.MoveMineTnt);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _manager.Player = null;
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
            }
        }
    }
}