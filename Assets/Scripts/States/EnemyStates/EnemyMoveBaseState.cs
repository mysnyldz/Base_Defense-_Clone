using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace States.EnemyStates
{
    public class EnemyMoveBaseState: EnemyBaseState
    {
        public override void EnterState(EnemyManager enemyManager)
        {
            enemyManager.agent.SetDestination(enemyManager.BasePoints.transform.position);
            enemyManager.SetTriggerAnim(EnemyAnimTypes.Walk);
        }
        
        public override void OnTriggerEnter(EnemyManager enemyManager, Collider other)
        {
            
        }
    }
}