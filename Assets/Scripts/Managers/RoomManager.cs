using System;
using System.Collections;
using Data.UnityObject;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using ValueObject;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private RoomTypes roomTypes;

        [SerializeField] private TextMeshPro roomPrice;
        [SerializeField] private GameObject roomPriceCollider;
        [SerializeField] private GameObject fences;
        [SerializeField] private GameObject room;
        [SerializeField] private int roomId;

        #endregion

        #region Private Variables
        
        private RoomData _roomData;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void Init()
        {
            RoomCostArea();
        }
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnSubscribeEvents()
        {
           
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        #endregion

        private void GetReferences()
        {
            _roomData = GetData();
            roomPrice.text = _roomData.RoomPrice.ToString();
        }
        private RoomData GetData()
        {
            return Resources.Load<CD_Structure>("Data/CD_BuildData").RoomData[(int)roomTypes];
        }

        private void RoomCostArea()
        {
            if (_roomData.RoomStageType == RoomStageTypes.Uncomplete)
            {
                roomPriceCollider.SetActive(true);
                room.SetActive(false);
                fences.SetActive(true);
            }
            else if (_roomData.RoomStageType == RoomStageTypes.Complete)
            {
                roomPriceCollider.SetActive(false);
                room.SetActive(true);
                fences.SetActive(false);
            }
        }
        
        private void SetAreaTexts()
        {
            roomPrice.text = (_roomData.RoomPrice - _roomData.PayedAmount).ToString();
        }
        
        private void RoomPriceDecrease()
        {
            switch (_roomData.RoomStageType)
            {
                case RoomStageTypes.Uncomplete:
                    _roomData.PayedAmount++;
                    SetAreaTexts();
                    if (_roomData.RoomPrice == _roomData.PayedAmount) ChangeStage();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (_roomData.RoomStageType == RoomStageTypes.Uncomplete)
            {
                _roomData.RoomStageType = RoomStageTypes.Complete;
                RoomSignals.Instance.onRoomComplete?.Invoke();
                StopTimer();
                RoomCostArea();
            }
            else
            {
                RoomCostArea();
            }
        }

        public void StartTimer()
        {
            StartCoroutine(Purchase());
        }
        public void StopTimer()
        {
            StopAllCoroutines();
        }

        IEnumerator Purchase()
        {
            float money = CurrencySignals.Instance.onGetMoney();
            WaitForSeconds timer = new WaitForSeconds(0.025f);
            while (money >= _roomData.RoomPrice)
            {
                RoomPriceDecrease();
                CurrencySignals.Instance.onReduceMoney?.Invoke(1);
                yield return timer;
                
            }

            yield return null;
        }
    }
}