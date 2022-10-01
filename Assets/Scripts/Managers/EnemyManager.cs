using Abstract;
using Controllers;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public NavMeshAgent agent;
        [ShowInInspector] public EnemyBaseState currentEnemyBaseState;
        public EnemyMoveBaseState EnemyMoveBaseState = new EnemyMoveBaseState();
        public EnemyMovePlayerState EnemyMovePlayerState = new EnemyMovePlayerState();
        public EnemyAttackState EnemyAttackState = new EnemyAttackState();
        public EnemyDeathState EnemyDeathState = new EnemyDeathState();
        public EnemyMoveMineTnt EnemyMoveMineTnt = new EnemyMoveMineTnt();
        public GameObject BasePoints;
        public GameObject Player;
        public GameObject MineTnt;

        #endregion

        #region Serializefield Variables

        [SerializeField] private EnemyAnimationController animationController;
        
        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        
        private void OnEnable()
        {
            BasePoints = EnemySignals.Instance.onGetBasePoints();
            Player = EnemySignals.Instance.onGetPlayerPoints();
            MineTnt = EnemySignals.Instance.onGetMineTntPoints();
            agent = GetComponent<NavMeshAgent>();
            currentEnemyBaseState = EnemyMoveBaseState;
            currentEnemyBaseState.EnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentEnemyBaseState.OnTriggerEnter(this, other);
        }

        public void SetTriggerAnim(EnemyAnimTypes types)
        {
            animationController.SetAnim(types);
        }


        public void SwitchState(EnemyStatesTypes state)
        {
            switch (state)
            {
                case EnemyStatesTypes.MoveBase:
                    currentEnemyBaseState = EnemyMoveBaseState;
                    break;
                case EnemyStatesTypes.MovePlayer:
                    currentEnemyBaseState = EnemyMovePlayerState;
                    break;
                case EnemyStatesTypes.Attack :
                    currentEnemyBaseState = EnemyAttackState;
                    break;
                case EnemyStatesTypes.MoveMineTnt:
                    currentEnemyBaseState = EnemyMoveMineTnt;
                    break;
                case EnemyStatesTypes.Death:
                    currentEnemyBaseState = EnemyDeathState;
                    break;
            }
            currentEnemyBaseState.EnterState(this);
        }
    }
}