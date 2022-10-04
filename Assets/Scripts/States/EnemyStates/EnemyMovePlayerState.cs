using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
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
            
        }

        public override void UpdateState()
        {
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                _agent.SetDestination(_manager.Player.transform.position);
                _manager.SetTriggerAnim(EnemyAnimTypes.Run);
            }

            if (other.CompareTag("Player"))
            {
                _manager.SetTriggerAnim(EnemyAnimTypes.Attack);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            
        }
    }
}