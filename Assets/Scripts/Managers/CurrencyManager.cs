using System;
using Abstract;
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

        
        #endregion

        #region SerilizeField Variables
        

        #endregion

        #region Private Variables
        
        private float _money; 
        private float _gem;
        private int _uniqueID;
        private CurrencyIdData _currencyIdData;

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
            CurrencySignals.Instance.onGetMoney += OnGetMoney;
            CurrencySignals.Instance.onGetGem += OnGetGem;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
        }
        


        private void UnsubscribeEvents()
        {
            CurrencySignals.Instance.onAddMoney -= OnAddMoney;
            CurrencySignals.Instance.onAddGem -= OnAddGem;
            CurrencySignals.Instance.onReduceMoney -= OnReduceMoney;
            CurrencySignals.Instance.onReduceGem -= OnReduceGem;
            CurrencySignals.Instance.onGetMoney -= OnGetMoney;
            CurrencySignals.Instance.onGetGem -= OnGetGem;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void Start()
        {
            Load();
            GetReferences();
            SetMoneyText();
            SetGemText();
        }
        

        private void GetReferences()
        {
            _money = _currencyIdData.Money;
            _gem = _currencyIdData.Gem;
        }


        private float OnGetMoney() => _currencyIdData.Money;
        private float OnGetGem() => _currencyIdData.Gem;

        private void OnAddMoney(float value)
        {
            _money += value;
            SetMoneyText();
            Save();
        }

        private void OnReduceMoney(float value)
        {
            _money -= value;
            SetMoneyText();
            Save();
        }

        private void OnAddGem(float value)
        {
            _gem += value;
            SetGemText();
            Save();
        }

        private void OnReduceGem(float value)
        {
            _gem -= value;
            SetGemText();
            Save();
        }

        private void SetMoneyText()
        {
            UISignals.Instance.onSetMoneyText?.Invoke(_money);
        }

        private void SetGemText()
        {
            UISignals.Instance.onSetGemText?.Invoke(_gem);
        }

        private void OnSave()
        {
            Save();
        }
        
        private void Save()
        {
            _currencyIdData = new CurrencyIdData(_money,_gem);
            SaveLoadSignals.Instance.onSaveCurrencyData.Invoke(_currencyIdData, _uniqueID);
        }

        public void Load()
        {
            _currencyIdData = SaveLoadSignals.Instance.onLoadCurrencyData(CurrencyIdData.CurrencyKey, _uniqueID);
        }
    }
}