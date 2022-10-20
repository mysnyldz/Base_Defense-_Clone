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
            roomData = GetRoomData();
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
                    roomIDData = new RoomIDData
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
            switch (roomIDData.RoomStageType)
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
            roomPriceTMP.text = (roomIDData.RoomPrice - roomIDData.PayedAmount).ToString();
        }

        private void RoomPriceDecrease()
        {
            switch (roomIDData.RoomStageType)
            {
                case RoomStageTypes.UnComplete:
                    roomIDData.PayedAmount++;
                    _money--;
                    SetAreaTexts();
                    if (roomIDData.RoomPrice == roomIDData.PayedAmount) ChangeStage();
                    Save();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (roomIDData.RoomStageType == RoomStageTypes.UnComplete)
            {
                roomIDData.RoomStageType = RoomStageTypes.Complete;
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
                if (_money >= roomIDData.RoomPrice)
                {
                    RoomPriceDecrease();
                    CurrencySignals.Instance.onReduceMoney?.Invoke(1);
                }

                if (roomIDData.RoomPrice - roomIDData.PayedAmount == 0)
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
        
        private void Save()
        {
            RoomIDData roomIDData = new RoomIDData(this.roomIDData.RoomTypes, this.roomIDData.RoomTurret, this.roomIDData.RoomID,
                this.roomIDData.RoomPrice, this.roomIDData.PayedAmount, this.roomIDData.RoomStageType);
            SaveLoadSignals.Instance.onSaveRoomData.Invoke(roomIDData, _uniqueID);
        }

        private void Load()
        {
            roomIDData = SaveLoadSignals.Instance.onLoadRoomData.Invoke(roomIDData.Key, _uniqueID);
        }

        #endregion
    }
}