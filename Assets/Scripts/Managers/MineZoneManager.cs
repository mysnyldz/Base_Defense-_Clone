﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using JetBrains.Annotations;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineZoneManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region SerializeField Variables

        [SerializeField] private GameObject gemDepot;
        [SerializeField] private List<GameObject> veins = new List<GameObject>();

        #endregion

        #region Private Variables

        private MineZoneData _zoneData;
        private List<int> _capacity;
        private int _rand;
        private int _gemDistance;
        private Vector3 _direct = Vector3.zero;
        private List<GameObject> _gemList = new List<GameObject>();
        private List<GameObject> _gemListCache;
        private CurrencyIdData _uniqueID;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onGetMineGemVeinTarget += OnGetMineGemVeinTarget;
            IdleSignals.Instance.onGetMineDepotTarget += OnGetMineDepotTarget;
            IdleSignals.Instance.onDepotAddGem += OnDepotAddGem;
        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onGetMineGemVeinTarget -= OnGetMineGemVeinTarget;
            IdleSignals.Instance.onGetMineDepotTarget -= OnGetMineDepotTarget;
            IdleSignals.Instance.onDepotAddGem -= OnDepotAddGem;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            _zoneData = GetMineData();
        }

        private MineZoneData GetMineData() => Resources.Load<CD_MineZoneData>("Data/CD_MineZoneData").Data;

        private void Start()
        {
            _capacity = new List<int>(new int[veins.Count]);
            _gemDistance = _zoneData.GemCountX * _zoneData.GemCountZ;
        }

        private GameObject OnGetMineGemVeinTarget()
        {
            while (true)
            {
                _rand = Random.Range(0, _capacity.Count);
                if (_capacity[_rand] != _zoneData.MaxMinerCapacity)
                {
                    _capacity[_rand]++;
                    break;
                }

                if (_capacity.Any(cap => cap != _zoneData.MaxMinerCapacity)) continue;
            }

            return veins[_rand];
        }

        private GameObject OnGetMineDepotTarget()
        {
            return gemDepot;
        }

        private void OnDepotAddGem(Transform miner)
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Gem);
            if (obj == null) return;
            obj.transform.parent = gemDepot.transform;
            obj.transform.position = new Vector3(miner.transform.position.x, miner.transform.position.y + 2,miner.transform.position.z);
            SetGemPosition(obj);
            obj.SetActive(true);
            _gemList.Add(obj);
        }

        private void SetGemPosition(GameObject gem)
        {
            _direct = _zoneData.GemInitPoint + gemDepot.transform.position;
            _direct.x = _direct.x + (int)(_gemList.Count % _zoneData.GemCountX)
                / _zoneData.OffsetFactor;
            _direct.y = _direct.y + (int)(_gemList.Count / _gemDistance)
                / _zoneData.OffsetFactor;
            _direct.z = _direct.z - (int)(_gemList.Count % _gemDistance / _zoneData.GemCountZ )
                / _zoneData.OffsetFactor;
            gem.transform.DOLocalMove(_direct, 0.5f);
        }

        public void PlayerEnterDepot(Transform other)
        {
            int count = _gemListCache.Count;
            _gemListCache = new List<GameObject>(_gemList);
            _gemList.Clear();
            for (int i = 0; i < count; i++)
            {
                var rand = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
                var obj = _gemListCache[i];
                obj.transform.SetParent(other);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(1f).OnComplete(() =>
                {
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Gem,obj);
                });
                CurrencySignals.Instance.onAddGem?.Invoke(1f);
            }
        }
        
    }
}