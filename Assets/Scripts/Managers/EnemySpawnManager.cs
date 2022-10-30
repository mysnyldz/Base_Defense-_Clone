using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        #region Self Variables


        #region Serializefield Variables

        [SerializeField] private List<GameObject> spawnPoints;
        [SerializeField] private List<GameObject> BasePoints;
        [SerializeField] private int timer;
        

        #endregion

        #endregion

        #region Private Variables

        private Dictionary<GameObject, List<SpawnData>> _baseSpawnDatas = new Dictionary<GameObject, List<SpawnData>>();
        private List<SpawnData> _spawnDatasCache;
        private SpawnData _spawnDataCache;
        private SpawnListData _data;
        private SpawnData _randomSpawnDataCache;
        private int _currentlevel;
        private float _enemyTimer = 0;

        private int _randomBasePoint;
        private int _randomBasePoints;
        private int _randomSpawnDatas;
        
        

        #endregion

        #region Event Subscription
        

        private void OnEnable()
        {
            SubscribeEvents();
            _currentlevel = BaseSignals.Instance.onGetBaseCount();
            _data = Resources.Load<CD_Base>("Data/CD_Base").baseData[_currentlevel].SpawnData;
        }

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onGetBasePoints += OnGetBasePoints;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onGetBasePoints -= OnGetBasePoints;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Start()
        {
            InitSpawnDictionary();
        }

        private void Update()
        {
            _enemyTimer += Time.deltaTime;
            if (_enemyTimer >= timer)
            {
                if (CurrentEnemyCheck())
                {
                    EnemySpawn();
                    _enemyTimer = 0;
                }
                
            }
        }
        
        private void InitSpawnDictionary()
        {
            EnemyDict();
        }

        private void EnemyDict()
        {
            foreach (var VARIABLE in BasePoints)
            {
                _spawnDatasCache = new List<SpawnData>();
                foreach (var spawnData in _data.SpawnDatas)
                {
                    _spawnDataCache = new SpawnData
                    {
                        EnemyTypes = spawnData.EnemyTypes,
                        EnemyCount = spawnData.EnemyCount,
                        CurrentEnemyCount = spawnData.CurrentEnemyCount
                    };
                    _spawnDatasCache.Add(_spawnDataCache);
                }
                _baseSpawnDatas.Add(VARIABLE, _spawnDatasCache);
            }
        }
        private bool CurrentEnemyCheck() => _baseSpawnDatas.Values.Any(SpawnDatas =>
            SpawnDatas.Any(spawnData => spawnData.CurrentEnemyCount < spawnData.EnemyCount));
        
        private void EnemySpawn()
        {
            RandomEnemy();
            PoolSignals.Instance.onGetPoolObject(_randomSpawnDataCache.EnemyTypes.ToString(),
                spawnPoints[Random.Range(0, spawnPoints.Count)].transform);
        }
        
        private void RandomEnemy()
        {
            while (true)
            {
                _randomBasePoints = Random.Range(0, BasePoints.Count);
                _randomSpawnDatas = Random.Range(0, _spawnDatasCache.Count);
                _randomSpawnDataCache = _baseSpawnDatas[BasePoints[_randomBasePoints]][_randomSpawnDatas];
                if (_randomSpawnDataCache.CurrentEnemyCount >= _randomSpawnDataCache.EnemyCount) continue;
                _baseSpawnDatas[BasePoints[_randomBasePoints]][_randomSpawnDatas].CurrentEnemyCount++;
                break;
            }
        }
        
        private GameObject OnGetBasePoints() => BasePoints[_randomBasePoints];


    }
}