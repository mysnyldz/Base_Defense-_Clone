using System;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector.Editor.GettingStarted;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private AreaType areaType;
        [SerializeField] private TextMeshPro areaPriceTMP;
        [SerializeField] private GameObject areaPriceCollider;
        [SerializeField] private GameObject fences;
        [SerializeField] private AreaData data;

        #endregion

        #region Private Variables

        private int _gem;
        private bool isCompleted;
        private int _roomPrice;
        private int _payedAmount;
        private AreaStageType _areaStageType;

        #endregion

        #endregion


        private void Start()
        {
            data = GetData();
            GetReferences();
        }


        private AreaData GetData() => Resources.Load<CD_AreaStageData>("Data/CD_AreaStageData").Data.AreaData[(int)areaType];

        private void GetReferences()
        {
            AreaCostArea();
        }
        
        
        private void AreaCostArea()
        {
            switch (data.AreaStageType)
            {
                case AreaStageType.Uncompleted:
                    areaPriceCollider.SetActive(true);
                    fences.SetActive(true);
                    SetAreaTexts();
                    break;
                case AreaStageType.Completed:
                    areaPriceCollider.SetActive(false);
                    fences.SetActive(false);
                    break;
            }
        }

        private void SetAreaTexts()
        {
            areaPriceTMP.text = (data.Price - data.PayedPrice).ToString();
        }
        
        private void AreaPriceDecrease()
        {
            switch (data.AreaStageType)
            {
                case AreaStageType.Uncompleted:
                    data.PayedPrice++;
                    _gem--;
                    SetAreaTexts();
                    if (data.Price == data.PayedPrice) ChangeStage();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (data.AreaStageType == AreaStageType.Uncompleted)
            {
                data.AreaStageType = AreaStageType.Completed;
                AreaCostArea();
            }
            else
            {
                AreaCostArea();
            }
        }

        public void OnBuyRoomArea()
        {
            _gem = CurrencySignals.Instance.onGetGem.Invoke();
            if (_areaStageType == AreaStageType.Uncompleted)
            {
                if (_gem >= data.Price)
                {
                    AreaPriceDecrease();
                    CurrencySignals.Instance.onReduceGem?.Invoke(1);
                }

                if (data.Price - data.PayedPrice == 0)
                {
                    _areaStageType = AreaStageType.Completed;
                }
            }
        }
    }
}