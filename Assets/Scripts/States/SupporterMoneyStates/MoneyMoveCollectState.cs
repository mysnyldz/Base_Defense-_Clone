using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.Diagnostics;

namespace States.SupporterMoneyStates
{
    public class MoneyMoveCollectState : SupporterBaseState
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        #endregion

        #region Private Variables

        private GameObject _target;
        private SupporterMoneyManager _manager;
        private NavMeshAgent _agent;
        private MoneyStackData _data;
        private int _stackListCount;

        #endregion

        #endregion

        public MoneyMoveCollectState(ref SupporterMoneyManager manager, ref NavMeshAgent agent,
            ref MoneyStackData moneyStackData)
        {
            _manager = manager;
            _agent = agent;
            _data = moneyStackData;
        }

        public override void EnterState()
        {
            _manager.SetTriggerAnim(SupporterAnimTypes.Walk);
        }

        public override void UpdateState()
        {
            _stackListCount = _manager.StackList.Count;
            if (_manager.SupporterAreaBuyManager.MoneyList != null)
            {
                if (_stackListCount >= _manager.MaxMoney)
                {
                    _target = null;
                    _manager.SwitchState(SupporterMoneyStateTypes.MoveBase);
                }

                _target = _manager.SupporterAreaBuyManager.MoneyList[0];
                _agent.SetDestination(_target.transform.position);
            }
            else if (_manager.SupporterAreaBuyManager.MoneyList.Count < 1)
            {
                _target = null;
                _manager.SwitchState(SupporterMoneyStateTypes.MoveBase);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                _manager.MoneyStackController.AddStack(other.gameObject);
            }
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}