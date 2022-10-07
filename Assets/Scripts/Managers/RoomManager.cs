using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
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

        #endregion

        #region Private Variables

        private float money = 0;
        private int _uniqueID;
        private bool isCompleted;
        private RoomTurretData _roomTurretData;
        private int roomID;
        private int roomPrice;
        private int payedAmount;
        private RoomStageTypes roomStageType;
        private CD_Structure _roomData;
        #endregion

        #endregion
        

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            RoomSignals.Instance.onInitializeRoom += OnInitializeRoom;
        }


        private void UnSubscribeEvents()
        {
            RoomSignals.Instance.onInitializeRoom -= OnInitializeRoom;
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
            roomID = (int)roomTypes;
            GetData();
            roomStageType = _roomData.Data.RoomIDList[(int)roomTypes].RoomStageType;
            payedAmount = _roomData.Data.RoomIDList[(int)roomTypes].PayedAmount;
            roomPrice = _roomData.Data.RoomIDList[(int)roomTypes].RoomPrice;

            RoomCostArea();
        }
        
        private void GetData()
        {
            if (!ES3.FileExists($"Room{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Room"))
                {
                    _roomData = GetRoomData();
                    Save();
                }
            }

            Load();
            _roomData = GetRoomData();
        }

        private CD_Structure GetRoomData() => Resources.Load<CD_Structure>("Data/CD_Structure");

        private void RoomCostArea()
        {
            switch (roomStageType)
            {
                case RoomStageTypes.Uncomplete:
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
            roomPriceTMP.text = (roomPrice - payedAmount).ToString();
        }

        private void RoomPriceDecrease()
        {
            Debug.Log("girdim");
            switch (roomStageType)
            {
                case RoomStageTypes.Uncomplete:
                    
                    payedAmount++;
                    money--;
                    SetAreaTexts();
                    if (roomPrice == payedAmount) ChangeStage();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (roomStageType == RoomStageTypes.Uncomplete)
            {
                roomStageType = RoomStageTypes.Complete;
                RoomSignals.Instance.onRoomComplete?.Invoke();
                RoomCostArea();
            }
            else
            {
                RoomCostArea();
            }
        }

        public void OnBuyRoomArea()
        {
            money = CurrencySignals.Instance.onGetMoney();
            if (roomStageType == RoomStageTypes.Uncomplete)
            {
                if (money >= roomPrice)
                {
                    RoomPriceDecrease();
                    CurrencySignals.Instance.onReduceMoney?.Invoke(1);
                }
                if (roomPrice - payedAmount == 0)
                {
                    roomStageType = RoomStageTypes.Complete;
                }
            }
        }
       private void OnInitializeRoom()
       {
           roomID = GetRoomCount();
       }

        private int GetRoomCount()
        {
            return roomID % Resources.Load<CD_Structure>("Data/CD_Structure").Data.RoomIDList.Count;
        }
        
        #region Room Save and Load

        public void Save()
        {
            RoomIDData roomIdData = new RoomIDData(roomTypes,_roomTurretData,roomID,roomPrice,payedAmount,roomStageType);
            SaveLoadSignals.Instance.onSaveRoomData.Invoke(roomIdData, _uniqueID);
        }

        public void Load()
        {
            RoomIDData roomIDData = SaveLoadSignals.Instance.onLoadRoomData.Invoke(RoomIDData.Key, _uniqueID);
        }

        #endregion
    }
}