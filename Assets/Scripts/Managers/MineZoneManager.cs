using System;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using JetBrains.Annotations;
using Keys;
using Signals;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
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
        [SerializeField] private GameObject tent;
        [SerializeField] private TextMeshPro minerTextCount;
        [SerializeField] private List<GameObject> veins = new List<GameObject>();
        [SerializeField] private List<GameObject> _gemList = new List<GameObject>();

        #endregion

        #region Private Variables

        private MineZoneData _zoneData;
        private List<int> _veinsListCache;
        private int _rand;
        private int _gemDistance;
        private Vector3 _direct = Vector3.zero;
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
            IdleSignals.Instance.onPlayerEnterGemDepot += OnPlayerEnterDepot;
            IdleSignals.Instance.onMineZoneAddMiner += OnMineZoneAddMiner;
        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onGetMineGemVeinTarget -= OnGetMineGemVeinTarget;
            IdleSignals.Instance.onGetMineDepotTarget -= OnGetMineDepotTarget;
            IdleSignals.Instance.onDepotAddGem -= OnDepotAddGem;
            IdleSignals.Instance.onPlayerEnterGemDepot -= OnPlayerEnterDepot;
            IdleSignals.Instance.onMineZoneAddMiner -= OnMineZoneAddMiner;
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
            _veinsListCache = new List<int>(new int[veins.Count]);
            _gemDistance = _zoneData.GemCountX * _zoneData.GemCountZ;
            SpawnMiner();
        }

        private GameObject OnGetMineGemVeinTarget()
        {
            while (true)
            {
                _rand = Random.Range(0, _veinsListCache.Count);
                if (_veinsListCache[_rand] != _zoneData.MaxMinerCapacity)
                {
                    _veinsListCache[_rand]++;
                    break;
                }

                if (_veinsListCache.Any(cap => cap != _zoneData.MaxMinerCapacity)) continue;
            }

            return veins[_rand];
        }

        private void SetText()
        {
            minerTextCount.text = (_zoneData.CurrentMinerAmount+"/"+_zoneData.MaxMinerCapacity);
        }

        private void OnMineZoneAddMiner()
        {
            if (_zoneData.CurrentMinerAmount < _zoneData.MaxMinerCapacity)
            {
                _zoneData.CurrentMinerAmount++;
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Miner.ToString(), tent.transform);
                SetText();
            }
        }

        private void SpawnMiner()
        {
            if (_zoneData.CurrentMinerAmount != 0)
            {
                for (int i = 0; i < _zoneData.CurrentMinerAmount; i++)
                {
                    PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Miner.ToString(), tent.transform);
                    SetText();
                }
            }
        }

        private GameObject OnGetMineDepotTarget()
        {
            return gemDepot;
        }

        private void OnDepotAddGem(GameObject obj)
        {
            var gem = PoolSignals.Instance.onGetPoolObject(PoolType.Gem.ToString(), obj.transform);
            if (gem == null) return;
            gem.transform.SetParent(gemDepot.transform);
            SetGemPosition(gem);
            _gemList.Add(gem);
        }

        private void SetGemPosition(GameObject gem)
        {
            _direct = _zoneData.GemInitPoint + gemDepot.transform.position;
            _direct.x = _direct.x + (_gemList.Count % _zoneData.GemCountX)
                / _zoneData.OffsetFactorX;
            _direct.z = _direct.z - (_gemList.Count / _gemDistance)
                / _zoneData.OffsetFactorZ;
            _direct.y = _direct.y + (_gemList.Count % _gemDistance / _zoneData.GemCountZ)
                / _zoneData.OffsetFactorY;
            gem.transform.DOLocalRotate(new Vector3(-90, 0, 0), 1).SetEase(Ease.OutQuad);
            gem.transform.DOLocalMove(new Vector3(_direct.x, _direct.y, _direct.z), 0.5f);
        }

        private void OnPlayerEnterDepot(GameObject obj)
        {
            PlayerEnterDepot(obj.transform);
        }

        public void PlayerEnterDepot(Transform other)
        {
            int limit = _gemList.Count;
            for (int i = 0; i < limit; i++)
            {
                var obj = _gemList[0];

                obj.transform.DOLocalMove(new Vector3(Random.Range(-1.5f, 1.5f), 0.75f, Random.Range(-1.5f, 1.5f)), 1f)
                    .SetEase(Ease.OutBack);
                obj.transform.SetParent(other);
                obj.transform.DOLocalRotate(Vector3.zero, 0.1f);
                obj.transform.DOLocalMove(new Vector3(0, 0.75f, 0), 0.5f).SetDelay(1f).OnComplete(() =>
                {
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Gem.ToString(), obj);
                });
                _gemList.Remove(obj);
                _gemList.TrimExcess();
            }

            CurrencySignals.Instance.onAddGem?.Invoke(limit);
        }
    }
}