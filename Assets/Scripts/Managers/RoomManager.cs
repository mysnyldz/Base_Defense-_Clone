using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using ES3Types;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private RoomTypes roomTypes;
        [SerializeField] private TextMeshPro roomPriceTMP;
        [SerializeField] private GameObject roomPriceCollider;
        [SerializeField] private GameObject fences;
        [SerializeField] private GameObject room;
        [SerializeField] private RoomIDData roomData;

        #endregion

        #region Private Variables

        private RoomIDData _roomDataCache;
        private RoomIDData roomIDData;
        private int _money;
        private int _uniqueID;
        private bool isCompleted;
        private RoomTurretData _roomTurretData;
        private int _roomID;
        private int _roomPrice;
        private int _payedAmount;
        private RoomStageTypes _roomStageType;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            // RoomSignals.Instance.onInitializeRoom += OnInitializeRoom;
        }


        private void UnSubscribeEvents()
        {
            //  RoomSignals.Instance.onInitializeRoom -= OnInitializeRoom;
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void Start()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _uniqueID = (int)roomTypes;
            _roomID = (int)roomTypes;
            GetData();
            RoomCostArea();
        }

        private void GetData()
        {
            if (!ES3.FileExists($"Room{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Room"))
                {
                    roomData = GetRoomData();
                    _roomDataCache = new RoomIDData
                    {
                        PayedAmount = roomData.PayedAmount,
                        RoomID = roomData.RoomID,
                        RoomPrice = roomData.RoomPrice,
                        RoomStageType = roomData.RoomStageType,
                        RoomTurret = roomData.RoomTurret,
                        RoomTypes = roomData.RoomTypes
                    };
                    Save();
                }
            }

            Load();
        }

        private RoomIDData GetRoomData() => Resources.Load<CD_Room>("Data/CD_Room").Data.RoomIDList[_uniqueID];

        private void RoomCostArea()
        {
            switch (_roomDataCache.RoomStageType)
            {
                case RoomStageTypes.UnComplete:
                    roomPriceCollider.SetActive(true);
                    room.SetActive(false);
                    fences.SetActive(true);
                    SetAreaTexts();
                    break;
                case RoomStageTypes.Complete:
                    roomPriceCollider.SetActive(false);
                    room.SetActive(true);
                    fences.SetActive(false);
                    break;
            }
        }

        private void SetAreaTexts()
        {
            roomPriceTMP.text = (_roomDataCache.RoomPrice - _roomDataCache.PayedAmount).ToString();
        }

        private void RoomPriceDecrease()
        {
            switch (_roomDataCache.RoomStageType)
            {
                case RoomStageTypes.UnComplete:
                    _roomDataCache.PayedAmount++;
                    _money--;
                    SetAreaTexts();
                    if (_roomDataCache.RoomPrice == _roomDataCache.PayedAmount) ChangeStage();
                    Save();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (_roomDataCache.RoomStageType == RoomStageTypes.UnComplete)
            {
                _roomDataCache.RoomStageType = RoomStageTypes.Complete;
                RoomSignals.Instance.onRoomComplete?.Invoke();
                RoomCostArea();
                Save();
            }
            else
            {
                RoomCostArea();
            }
        }

        public void OnBuyRoomArea()
        {
            _money = CurrencySignals.Instance.onGetMoney.Invoke();
            if (_roomStageType == RoomStageTypes.UnComplete)
            {
                if (_money >= _roomDataCache.RoomPrice)
                {
                    RoomPriceDecrease();
                    CurrencySignals.Instance.onReduceMoney?.Invoke(1);
                }

                if (_roomDataCache.RoomPrice - _roomDataCache.PayedAmount == 0)
                {
                    _roomStageType = RoomStageTypes.Complete;
                    Save();
                }
            }
        }
        //private void OnInitializeRoom()
        //{
        //    _roomID = GetRoomCount();
        //}

        // private int GetRoomCount()
        // {
        //     return _roomID % Resources.Load<CD_Room>("Data/CD_Room").Data.RoomIDList.Count;
        // }

        #region Room Save and Load

        [Button]
        private void Save()
        {
            RoomIDData roomIdData = new RoomIDData(_roomDataCache.RoomTypes, _roomDataCache.RoomTurret, _roomDataCache.RoomID,
                _roomDataCache.RoomPrice, _roomDataCache.PayedAmount, _roomDataCache.RoomStageType);
            SaveLoadSignals.Instance.onSaveRoomData.Invoke(roomIdData, _uniqueID);
        }

        private void Load()
        {
            RoomIDData roomIDData = SaveLoadSignals.Instance.onLoadRoomData.Invoke(this.roomIDData.GetKey(), _uniqueID);
        }

        #endregion
    }
}