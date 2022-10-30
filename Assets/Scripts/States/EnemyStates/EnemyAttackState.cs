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
        private bool _isAttacked = true;
        private float _timer = 0;
        private float _playerDelayTime = 1;
        private float _baseDelayTime = 4;

        #endregion

        #endregion

        public EnemyAttackState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data,
            ref EnemyTypes enemyTypes)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = enemyTypes;
        }


        public override void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
        }

        public override void UpdateState()
        {
            if (_manager.Health())
            {
                _isAttacked = false;
                _manager.Player = null;
                _manager.SetTriggerAnim(EnemyAnimTypes.Death);
                _manager.SwitchState(EnemyStatesTypes.Death);
            }

            else if (_isAttacked && _manager.Player != null)
            {
                AttackedPlayer();
            }
            else if (_isAttacked && _manager.Player == null)
            {
                AttackedBase();
            }
            else if (_isAttacked == false && _manager.Player != null)
            {
                _manager.SwitchState(EnemyStatesTypes.MovePlayer);
            }
            else if (_isAttacked == false && _manager.Player == null)
            {
                _manager.SwitchState(EnemyStatesTypes.MoveBase);
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
                _agent.destination = _manager.Player.transform.position;
            }

            if (other.CompareTag("MineTargetSphere"))
            {
                _manager.MineTnt = other.transform.parent.gameObject;
                _manager.SwitchState(EnemyStatesTypes.MoveMineTnt);
            }

            if (other.CompareTag("EnemyBasePoints"))
            {
                _isAttacked = true;
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _manager.Player = null;
                _isAttacked = false;
                PlayerSignals.Instance.onEnemyRemoveTargetList.Invoke(_agent.gameObject);
            }

            if (other.CompareTag("Player"))
            {
                _isAttacked = false;
            }

            if (other.CompareTag("EnemyBasePoints"))
            {
                _isAttacked = false;
            }
        }

        private void AttackedPlayer()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                if ((_manager.transform.position-_manager.Player.transform.position).sqrMagnitude <= Mathf.Pow(_data.EnemyTypeDatas[_types].AttackRange,2))
                {
                    _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
                    PlayerSignals.Instance.onHealthUpdate?.Invoke(_data.EnemyTypeDatas[_types].Damage);
                }

                else if ((_manager.transform.position-_manager.Player.transform.position).sqrMagnitude >= Mathf.Pow(_data.EnemyTypeDatas[_types].AttackRange,2))
                {
                    _isAttacked = false;
                }

                _timer = 0;
            }
        }

        private void AttackedBase()
        {
            _timer += Time.deltaTime;
            if (_timer >= 2.5f)
            {
                
                if ((_manager.transform.position-_manager.BasePoints.transform.position).sqrMagnitude <= Mathf.Pow((_data.EnemyTypeDatas[_types].AttackRange*2),2))
                {
                    _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
                }

                else if ((_manager.transform.position-_manager.BasePoints.transform.position).sqrMagnitude >= Mathf.Pow((_data.EnemyTypeDatas[_types].AttackRange*2),2))
                {
                    _isAttacked = false;
                }

                _timer = 0;
            }
        }
    }
}