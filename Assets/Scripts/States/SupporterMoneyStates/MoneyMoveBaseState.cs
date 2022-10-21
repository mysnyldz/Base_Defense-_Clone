using System.Threading.Tasks;
using Abstract;
using Data.ValueObject;
using DG.Tweening;
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
            _manager.IsInSafe = false;
            _manager.SetTriggerAnim(SupporterAnimTypes.Walk);
            _agent.SetDestination(_manager.BasePoint.transform.position);
        }

        public override void UpdateState()
        {
            if (_manager.SupporterAreaBuyManager.MoneyList.Count >= 1 && _manager.IsInSafe == true)
            {
                _manager.SwitchState(SupporterMoneyStateTypes.MoveMoney);
            }
            else
            {
                _agent.SetDestination(_manager.BasePoint.transform.position);
            }
        }

        public override async void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoneySafe"))
            {
                var takenPoint = _manager.SupporterAreaBuyManager.MoneyTakenPoint.transform.position;
                await Task.Delay(500);
                _agent.transform.DOLookAt(
                    new Vector3(takenPoint.x, 0f,
                        takenPoint.z), 1f);
                _manager.SetTriggerAnim(SupporterAnimTypes.Gather);
                _manager.MoneyStackController.DecreaseStack();
                await Task.Delay(1000);
                _manager.SetTriggerAnim(SupporterAnimTypes.Idle);
                await Task.Delay(1000);
                _manager.IsInSafe = true;
            }
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}