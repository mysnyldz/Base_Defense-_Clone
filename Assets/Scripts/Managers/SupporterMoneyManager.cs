using System;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Controllers.SupporterControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using States.SupporterMoneyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class SupporterMoneyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject BasePoint;
        public GameObject Target;
        public MoneyStackController MoneyStackController;
        [ShowInInspector] public List<GameObject> StackList = new List<GameObject>();
        public bool IsInSafe = false;
        public int MaxMoney;
        public SupporterAreaManager SupporterAreaBuyManager;

        #endregion

        #region Serializefield Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private SupporterMoneyAnimationController animationController;
        
        

        #endregion

        #region Private Variables

        [ShowInInspector] private SupporterBaseState _currentMoneyBaseState;
        private MoneyMoveBaseState _moneyMoveBaseState;
        private MoneyMoveCollectState _moneyMoveCollectState;
        private MoneyWaitState _moneyWaitState;
        private MoneyStackData _data;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private MoneyStackData GetSupporterData() => Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;


        #region Event Subscription

        private void OnEnable()
        {
            BasePoint = IdleSignals.Instance.onMoneySupporterBasePoints.Invoke();
            _currentMoneyBaseState = _moneyMoveBaseState;
            _currentMoneyBaseState.EnterState();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }


        private void UnsubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetReferences()
        {
            var manager = this;
            _moneyMoveBaseState = new MoneyMoveBaseState(ref manager, ref agent, ref _data);
            _moneyMoveCollectState = new MoneyMoveCollectState(ref manager, ref agent, ref _data);
            _moneyWaitState = new MoneyWaitState(ref manager, ref agent, ref _data);
            _data = GetSupporterData();
            MaxMoney = _data.SupporterMaxMoneyCount;
            GetSupporterAreaManager();
        }
        
        private void Update()
        {
            _currentMoneyBaseState.UpdateState();
            StackList = MoneyStackController.StackList;
        }

        private void GetSupporterAreaManager()
        {
            SupporterAreaBuyManager = IdleSignals.Instance.onGetSupporterAreaManager?.Invoke();
        }
        

        public void SwitchState(SupporterMoneyStateTypes state)
        {
            switch (state)
            {
                case SupporterMoneyStateTypes.MoveBase:
                    _currentMoneyBaseState = _moneyMoveBaseState;
                    break;
                case SupporterMoneyStateTypes.MoveMoney:
                    _currentMoneyBaseState = _moneyMoveCollectState;
                    break;
                case SupporterMoneyStateTypes.Idle:
                    _currentMoneyBaseState = _moneyWaitState;
                    break;
            }

            _currentMoneyBaseState.EnterState();
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentMoneyBaseState.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _currentMoneyBaseState.OnTriggerExit(other);
        }
        public void SetTriggerAnim(SupporterAnimTypes animTypes)
        {
            animationController.SetAnim(animTypes);
        }
    }
}