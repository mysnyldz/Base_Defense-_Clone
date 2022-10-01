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

        #region SerilizeField
        
        

        #endregion

        #region Private Variables
        
        private float _money = 200; 
        private float _gem = 1520;
        private int _uniqueID = 1;
        

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
            LoadData();
            SetMoneyText();
            SetGemText();
        }

        private float OnGetMoney() => _money;
        private float OnGetGem() => _gem;

        private void OnAddMoney(float value)
        {
            _money += value;
            SetMoneyText();
            SaveData();
        }

        private void OnReduceMoney(float value)
        {
            _money -= value;
            SetMoneyText();
            SaveData();
        }

        private void OnAddGem(float value)
        {
            _gem += value;
            SetGemText();
            SaveData();
        }

        private void OnReduceGem(float value)
        {
            _gem -= value;
            SetGemText();
            SaveData();
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
            SaveData();
        }
        
        private void SaveData()
        {
            CurrencyIdData currencyIdData = new CurrencyIdData(_money,_gem);
            SaveLoadSignals.Instance.onSaveCurrencyData.Invoke(currencyIdData, _uniqueID);
        }

        public void LoadData()
        {
            CurrencyIdData data = SaveLoadSignals.Instance.onLoadCurrencyData(CurrencyIdData.CurrencyKey, _uniqueID);
        }
    }
}