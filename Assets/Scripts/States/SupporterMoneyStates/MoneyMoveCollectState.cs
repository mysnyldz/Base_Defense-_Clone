using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
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

        private SupporterMoneyManager _manager;
        private NavMeshAgent _agent;
        private MoneyStackData _data;
        private GameObject _target;

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
            if (_manager.MoneyList != null)
            {
                _target = _manager.MoneyList[0];
                _manager.SetTriggerAnim(SupporterAnimTypes.Walk);
                _agent.SetDestination(_target.transform.position);
                if (_manager.MoneyStackController.StackList.Count >= _data.SupporterMaxMoneyCount)
                {
                    _manager.SwitchState(SupporterMoneyStateTypes.MoveBase);
                }
            }
            else if (_manager.MoneyList == null)
            {
                _target = null;
                _manager.SetTriggerAnim(SupporterAnimTypes.Idle);
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