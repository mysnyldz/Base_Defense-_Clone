using System.Threading.Tasks;
using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace States.EnemyStates
{
    public class EnemyAttackState : EnemyBaseState
    {
        public override void EnterState(EnemyManager enemyManager)
        {
        }

        public override void OnTriggerEnter(EnemyManager enemyManager, Collider other)
        {
            
        }

        private async void Timer(EnemyManager enemyManager)
        {
            await Task.Delay(2750);
            enemyManager.SwitchState(EnemyStatesTypes.Attack);
        }
    }
}