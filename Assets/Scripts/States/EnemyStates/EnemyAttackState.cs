using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyAttackState : EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyTypesData _data;
        #endregion

        #endregion

        public EnemyAttackState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyTypesData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }
        
        
        public override void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
            _manager.Attack(true);
        }

        public override void UpdateState()
        {
            _agent.destination = _manager.Player.transform.position;
            if (_agent.remainingDistance >= _agent.stoppingDistance)
            {
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
            }

            if (_manager.Health())
            {
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _manager.Player = EnemySignals.Instance.onGetPlayerPoints?.Invoke();
                _manager.BasePoints = null;
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
                
            }
        }

        public override void OnTriggerExit( Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _manager.Attack(false);
                _manager.BasePoints = EnemySignals.Instance.onGetBasePoints?.Invoke();
                _manager.Player = null;
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
            }
        }
        
    }
}