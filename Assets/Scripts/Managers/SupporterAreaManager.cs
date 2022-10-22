using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SupporterAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject MoneySupporterBasePoint;
        public List<GameObject> MoneyList = new List<GameObject>();
        public GameObject MoneyTakenPoint;

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject moneyWorkerButton;
        [SerializeField] private GameObject ammoWorkerButton;

        #endregion

        #region Private Variables

        private int _newValue = 0;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onMoneySupporterBasePoints += OnMoneySupporterBasePoints;
            IdleSignals.Instance.onMoneySupporterBuyArea += OnMoneySupporterBuyArea;
            IdleSignals.Instance.onGetSupporterAreaManager += OnGetSupporterAreaManager;
            IdleSignals.Instance.onMoneyGetTakenPoints += OnMoneyGetTakenPoints;
            IdleSignals.Instance.onAddMoneyList += OnAddMoneyList;
            IdleSignals.Instance.onRemoveMoneyList += OnRemoveMoneyList;
        }



        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onMoneySupporterBasePoints -= OnMoneySupporterBasePoints;
            IdleSignals.Instance.onMoneySupporterBuyArea -= OnMoneySupporterBuyArea;
            IdleSignals.Instance.onGetSupporterAreaManager -= OnGetSupporterAreaManager;
            IdleSignals.Instance.onMoneyGetTakenPoints -= OnMoneyGetTakenPoints;
            IdleSignals.Instance.onAddMoneyList -= OnAddMoneyList;
            IdleSignals.Instance.onRemoveMoneyList -= OnRemoveMoneyList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private GameObject OnMoneySupporterBasePoints()
        {
            return MoneySupporterBasePoint;
        }

        private void OnMoneySupporterBuyArea(int value)
        {
            _newValue += value;
            if (_newValue <= 1)
            {
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.MoneySupporter.ToString(),
                    moneyWorkerButton.transform);
            }
        }
        private SupporterAreaManager OnGetSupporterAreaManager()
        {
            return this;
        }
        private GameObject OnMoneyGetTakenPoints()
        {
            return MoneyTakenPoint;
        }
        
        private void OnAddMoneyList(GameObject money)
        {
            MoneyList.Add(money);
        }

        private void OnRemoveMoneyList(GameObject money)
        {
            MoneyList.Remove(money);
            MoneyList.TrimExcess();
        }
    }
}
