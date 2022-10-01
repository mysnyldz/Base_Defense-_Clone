using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace States.EnemyStates
{
    public class EnemyMovePlayerState: EnemyBaseState
    {
        public override void EnterState(EnemyManager enemyManager)
        {
            
        }
        
        public override void OnTriggerEnter(EnemyManager enemyManager, Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
            {
                enemyManager.agent.SetDestination(enemyManager.Player.transform.position);
                enemyManager.SetTriggerAnim(EnemyAnimTypes.Run);
            }

            if (other.CompareTag("Player"))
            {
                enemyManager.SetTriggerAnim(EnemyAnimTypes.Attack);
            }
        }
    }
}