using System;
using System.Threading.Tasks;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class HostageController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private HostageManager manager;

        #endregion

        #region Private Variables

        private int _maxMinerCapacity;
        private int _currentMinerCount;

        #endregion

        #endregion

        private void Start()
        {
            _maxMinerCapacity = manager.MaxMinerCount;
            _currentMinerCount = manager.CurrentMinerAmount;
        }

        public async void TurnToMiner()
        {
            if (_currentMinerCount < _maxMinerCapacity)
            {
                manager.SetBoolAnimation(HostageAnimTypes.Walk, true);
                manager.agent.SetDestination(manager.Tent.transform.position);
                await Task.Delay(3000);
                manager.SwitchState(HostageStatesTypes.Idle);
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Hostage.ToString(), manager.gameObject);
                IdleSignals.Instance.onMineZoneAddMiner?.Invoke();
            }
            
        }
    }
}