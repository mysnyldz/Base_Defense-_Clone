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
        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onMoneySupporterBasePoints -= OnMoneySupporterBasePoints;
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
            Debug.Log("once" + _newValue);
            _newValue += value;
            Debug.Log("sonra" + _newValue);
            if (_newValue <= 1)
            {
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.MoneySupporter.ToString(),
                    moneyWorkerButton.transform);
            }
        }
    }
}