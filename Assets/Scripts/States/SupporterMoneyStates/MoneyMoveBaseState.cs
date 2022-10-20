using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.SupporterMoneyStates
{
    public class MoneyMoveBaseState : SupporterBaseState
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
        private float _timer;

        #endregion

        #endregion

        public MoneyMoveBaseState(ref SupporterMoneyManager manager, ref NavMeshAgent agent,
            ref MoneyStackData moneyStackData)
        {
            _manager = manager;
            _agent = agent;
            _data = moneyStackData;
        }

        public override void EnterState()
        {
            _manager.SetTriggerAnim(SupporterAnimTypes.Walk);
            _agent.SetDestination(_manager.BasePoint.transform.position);
        }

        public override void UpdateState()
        {
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoneySafe"))
            {
                _agent.transform.LookAt(other.transform.position);
                _manager.SetTriggerAnim(SupporterAnimTypes.Idle);
                _timer += Time.deltaTime;
                if (_timer > 2f)
                {
                    _timer = 0;
                    if (_manager.MoneyList != null)
                    {
                        _manager.SwitchState(SupporterMoneyStateTypes.MoveMoney);
                    }
                }
            }
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}