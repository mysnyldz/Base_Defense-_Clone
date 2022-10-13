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
        private EnemyData _data;
        private EnemyTypes _types;
        private bool _isAttacked;
        private float _timer = 0;
        private float _delayTime = 1;

        #endregion

        #endregion

        public EnemyAttackState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }


        public override void EnterState()
        {
            _isAttacked = true;
        }

        public override void UpdateState()
        {
            if (_isAttacked == true)
            {
                Attacked();
            }

            if (_manager.Health())
            {
                _isAttacked = false;
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isAttacked = true;
            }

            if (other.CompareTag("PlayerSphere"))
            {
                PlayerSignals.Instance.onEnemyAddTargetList.Invoke(_agent.gameObject);
                _manager.Player = other.transform.parent.gameObject;
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
                _isAttacked = false;
            }
            
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                PlayerSignals.Instance.onEnemyRemoveTargetList.Invoke(_agent.gameObject);
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
            }

            if (other.CompareTag("Player"))
            {
                _isAttacked = false;
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
                
            }

            if (other.CompareTag("EnemyBasePoints"))
            {
                _isAttacked = false;
            }
        }

        private void Attacked()
        {
            _timer += (Time.fixedDeltaTime) * 0.4f;
            if (_timer >= _delayTime)
            {
                _timer = 0;
                _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
            }
        }
    }
}