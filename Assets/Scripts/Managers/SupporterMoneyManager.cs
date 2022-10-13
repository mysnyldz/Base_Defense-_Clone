using System;
using Abstract;
using Controllers;
using Controllers.SupporterControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using States.SupporterMoneyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class SupporterMoneyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public SupporterBaseState CurrentMoneyState;

        #endregion

        #region Serializefield Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private SupporterMoneyAnimationController animationController;
        [SerializeField] private MoneyStackController stackController;

        #endregion

        #region Private Variables

        private MoneyBaseState _moneyBaseState;
        private MoneyCollectState _moneyCollectState;
        private MoneyWaitState _moneyWaitState;
        private MoneyStackData _data;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            
        }

        private MoneyStackData GetSupporterData() => Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;

        private void OnEnable()
        {
            CurrentMoneyState.EnterState();
        }
        
        private void GetReferences()
        {
            var manager = this;
            _moneyBaseState = new MoneyBaseState(ref manager, ref agent);
            _moneyCollectState = new MoneyCollectState(ref manager,ref agent);
            _moneyWaitState = new MoneyWaitState(ref manager, ref agent);
            _data = GetSupporterData();
        }

        private void Start()
        {
            CurrentMoneyState.EnterState();
        }

        private void Update()
        {
            CurrentMoneyState.UpdateState();
        }

        public void SetTriggerAnim(SupporterAnimTypes animTypes)
        {
            animationController.SetAnim(animTypes);
        }
        
        
        
        
        
        
        
        
        
        
    }
}