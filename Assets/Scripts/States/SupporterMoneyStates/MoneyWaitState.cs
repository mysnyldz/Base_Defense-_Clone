using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.SupporterMoneyStates
{
    public class MoneyWaitState : SupporterBaseState
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        #endregion

        #region Private Variables

        private SupporterMoneyManager _manager;
        private NavMeshAgent _agent;
        private MoneyStackData _data;

        #endregion

        #endregion

        public MoneyWaitState(ref SupporterMoneyManager manager, ref NavMeshAgent agent,
            ref MoneyStackData moneyStackData)
        {
            _manager = manager;
            _agent = agent;
            _data = moneyStackData;
        }

        public override void EnterState()
        {
            _manager.SetTriggerAnim(SupporterAnimTypes.Idle);
        }

        public override void UpdateState()
        {
            if (_manager.SupporterAreaBuyManager.MoneyList.Count >= 1)
            {
                _manager.SwitchState(SupporterMoneyStateTypes.MoveMoney);
            }
            else
            {
                _manager.SwitchState(SupporterMoneyStateTypes.MoveBase);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}