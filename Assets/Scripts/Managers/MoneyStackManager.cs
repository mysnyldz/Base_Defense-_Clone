using System.Collections.Generic;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MoneyStackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private MoneyStackController moneyStackController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleSignals.Instance.onGetMoney += OnGetMoney;
            IdleSignals.Instance.onGetMoneyManager += OnGetMoneyManager;
            IdleSignals.Instance.onGetMoneyController += OnGetMoneyController;
        }



        private void UnsubscribeEvent()
        {
            IdleSignals.Instance.onGetMoney -= OnGetMoney;
            IdleSignals.Instance.onGetMoneyManager -= OnGetMoneyManager;
            IdleSignals.Instance.onGetMoneyController -= OnGetMoneyController;
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        #endregion

        public void AddStack(GameObject obj)
        {
            moneyStackController.AddStack(obj);
        }

        private GameObject OnGetMoney()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Money, transform);
            return obj;
        }
        private GameObject OnGetMoneyController()
        {
            return moneyStackController.gameObject;
        }

        private GameObject OnGetMoneyManager()
        {
            return gameObject;
        }
    }
}
