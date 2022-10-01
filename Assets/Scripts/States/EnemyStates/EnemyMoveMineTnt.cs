using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace States.EnemyStates
{
    public class EnemyMoveMineTnt: EnemyBaseState
    {
        public override void EnterState(EnemyManager enemyManager)
        {
            enemyManager.agent.SetDestination(enemyManager.MineTnt.transform.position);
            enemyManager.SetTriggerAnim(EnemyAnimTypes.Run);
        }
        
        public override void OnTriggerEnter(EnemyManager enemyManager, Collider other)
        {
            
        }
    }
}