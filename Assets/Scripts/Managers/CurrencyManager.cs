using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        [Header("CurrencyIdData")] public CurrencyIdData CurrenctIdData;

        #endregion

        #region SerilizeField

        #endregion

        #region Private Variables

        private float _money;
        private float _gem;
        

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CurrencySignals.Instance.onAddMoney += OnAddMoney;
            CurrencySignals.Instance.onAddGem += OnAddGem;
            CurrencySignals.Instance.onReduceMoney += OnReduceMoney;
            CurrencySignals.Instance.onReduceGem += OnReduceGem;
        }


        private void UnsubscribeEvents()
        {
            CurrencySignals.Instance.onAddMoney -= OnAddMoney;
            CurrencySignals.Instance.onAddGem -= OnAddGem;
            CurrencySignals.Instance.onReduceMoney -= OnReduceMoney;
            CurrencySignals.Instance.onReduceGem -= OnReduceGem;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnAddMoney(float value)
        {
            _money += value;
            SetMoneyText();
        }

        private void OnReduceMoney(float value)
        {
            _money -= value;
            SetMoneyText();
        }

        private void OnAddGem(float value)
        {
            _gem += value;
            SetGemText();
        }

        private void OnReduceGem(float value)
        {
            _gem -= value;
            SetGemText();
        }

        private void SetMoneyText()
        {
            UISignals.Instance.onSetMoneyText?.Invoke(_money);
        }

        private void SetGemText()
        {
            UISignals.Instance.onSetGemText?.Invoke(_gem);
        }
        
        private CurrencyIdData OnSaveCurrencyData()
        {
            return new CurrencyIdData()
            {
                MoneyId = _money,
                GemId = _gem
            };
        }

        public void LoadData(int uniqueId)
        {
            CurrencyIdData data = SaveLoadSignals.Instance.onLoadCurrencyData(CurrencyIdData.CurrencyKey, uniqueId);
            _money = data.MoneyId;
            _gem = data.GemId;
        }
    }
}