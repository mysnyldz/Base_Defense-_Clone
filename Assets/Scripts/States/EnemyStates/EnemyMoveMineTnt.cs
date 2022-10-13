using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMoveMineTnt: EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyData _data;
        private EnemyTypes _types;
        #endregion

        #endregion

        public EnemyMoveMineTnt(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
        }
        public override void EnterState()
        {
            _agent.speed = _data.EnemyTypeDatas[_types].RunSpeed;
            _agent.SetDestination(_manager.MineTnt.transform.position);
            _manager.SetTriggerAnim(EnemyAnimTypes.Run);
        }

        public override void UpdateState()
        {
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            
        }

        public override void OnTriggerExit(Collider other)
        {
            
        }
    }
}