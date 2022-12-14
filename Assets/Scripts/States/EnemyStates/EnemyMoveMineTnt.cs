using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMoveMineTnt : EnemyBaseState
    {
        #region Self Variables

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyData _data;
        private EnemyTypes _types;

        #endregion

        #endregion

        public EnemyMoveMineTnt(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data,
            ref EnemyTypes enemyTypes)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = enemyTypes;
        }

        public override void EnterState()
        {
            _agent.speed = _data.EnemyTypeDatas[_types].RunSpeed;
            _agent.SetDestination(_manager.MineTnt.transform.position);
            _manager.SetTriggerAnim(EnemyAnimTypes.Run);
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
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MineTargetSphere"))
            {
                _manager.MineTnt = null;
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
            }
        }
    }
}