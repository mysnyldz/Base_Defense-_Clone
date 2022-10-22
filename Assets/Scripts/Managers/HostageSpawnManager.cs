using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class HostageSpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private List<GameObject> spawnPoints;
        [SerializeField] private List<GameObject> _hostageControllList;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> hostageList = new List<GameObject>();
        private float _hostageTimer;
        private int _objIndex;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            SpawnController();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onRemoveHostageSpawnPoint += OnRemoveHostageSpawnPoint;
        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onRemoveHostageSpawnPoint -= OnRemoveHostageSpawnPoint;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private async void SpawnController()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                await Task.Delay(5000);
                if (_hostageControllList[i] == null && hostageList.Count <= 6)
                {
                    AddHostageList(i);
                }
            }
        }
        

        private void OnRemoveHostageSpawnPoint(GameObject obj)
        {
            var index = hostageList.IndexOf(obj);
            RemoveHostageList(index);
        }

        private void AddHostageList(int i)
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Hostage.ToString(), spawnPoints[i].transform);
            hostageList.Add(obj);
            hostageList[i] = obj;
            _hostageControllList[i] = hostageList[i];
        }

        private void RemoveHostageList(int i)
        {
            hostageList[i] = null;
            SpawnController();
        }
    }
}