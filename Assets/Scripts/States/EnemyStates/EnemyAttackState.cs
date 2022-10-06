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
        private bool _isAttacked;
        private float _timer = 0;
        private float _delayTime = 1;

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
                _manager.Player = other.transform.parent.gameObject;
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
                _isAttacked = false;
            }
            
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
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