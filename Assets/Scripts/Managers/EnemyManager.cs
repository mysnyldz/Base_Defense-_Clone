using System;
using System.Collections;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
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

        public GameObject BasePoints;
        public GameObject Player;
        public GameObject MineTnt;

        #endregion

        #region Serializefield Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyTypes types;
        [SerializeField] private EnemyAnimationController animationController;

        #endregion

        #region Private Variables

        private EnemyBaseState _currentEnemyBaseState;
        private EnemyMoveBaseState _enemyMoveBaseState;
        private EnemyMovePlayerState _enemyMovePlayerState;
        private EnemyAttackState _enemyAttackState;
        private EnemyDeathState _enemyDeathState;
        private EnemyMoveMineTnt _enemyMoveMineTnt;
        private EnemyTypesData _enemyTypesData;

        #endregion

        #endregion

        private void Awake()
        {
            var manager = this;
            _enemyTypesData = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData.EnemyTypeDatas[types];
            _enemyMoveBaseState = new EnemyMoveBaseState(ref manager, ref agent, ref _enemyTypesData);
            _enemyMovePlayerState = new EnemyMovePlayerState(ref manager, ref agent, ref _enemyTypesData);
            _enemyAttackState = new EnemyAttackState(ref manager, ref agent, ref _enemyTypesData);
            _enemyDeathState = new EnemyDeathState(ref manager, ref agent, ref _enemyTypesData);
            _enemyMoveMineTnt = new EnemyMoveMineTnt(ref manager, ref agent, ref _enemyTypesData);
        }


        private void OnEnable()
        {
            BasePoints = EnemySignals.Instance.onGetBasePoints?.Invoke();
            Player = EnemySignals.Instance.onGetPlayerPoints?.Invoke();
            MineTnt = EnemySignals.Instance.onGetMineTntPoints?.Invoke();
            agent = GetComponent<NavMeshAgent>();
            _currentEnemyBaseState.EnterState();
        }

        private void OnDisable()
        {
            BasePoints = null;
            Player = null;
            MineTnt = null;
            _currentEnemyBaseState = _enemyMoveBaseState;
        }

        private void Start()
        {
            _currentEnemyBaseState.EnterState();
        }

        private void Update()
        {
            _currentEnemyBaseState.UpdateState();
        }


        private void OnTriggerEnter(Collider other)
        {
            _currentEnemyBaseState.OnTriggerEnter(other);
        }

        public void SetTriggerAnim(EnemyAnimTypes types)
        {
            animationController.SetAnim(types);
        }

        public IEnumerator AtackDelayTime()
        {
            yield return new WaitForSeconds(1.5f);
            SwitchState(EnemyStatesTypes.Attack);
        }


        public void SwitchState(EnemyStatesTypes state)
        {
            switch (state)
            {
                case EnemyStatesTypes.MoveBase:
                    _currentEnemyBaseState = _enemyMoveBaseState;
                    break;
                case EnemyStatesTypes.MovePlayer:
                    _currentEnemyBaseState = _enemyMovePlayerState;
                    break;
                case EnemyStatesTypes.Attack:
                    _currentEnemyBaseState = _enemyAttackState;
                    break;
                case EnemyStatesTypes.MoveMineTnt:
                    _currentEnemyBaseState = _enemyMoveMineTnt;
                    break;
                case EnemyStatesTypes.Death:
                    _currentEnemyBaseState = _enemyDeathState;
                    break;
            }

            _currentEnemyBaseState.EnterState();
        }
    }
}