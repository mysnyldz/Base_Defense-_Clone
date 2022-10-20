using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers
{
    public class TurretDepotController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> _ammoList = new List<GameObject>();

        #endregion

        #region Serializefield Variables

        [SerializeField] private TurretManager manager;
        [SerializeField] private AmmoStackController ammoStackController;

        #endregion

        #region Private Variables

        private List<GameObject> _ammoStackList = new List<GameObject>();
        private TurretDepotAmmoData _zoneData;
        private List<int> _capacity;
        private int _ammoDistance;
        private Vector3 _direct = Vector3.zero;
        private GameObject turretDepot;
        private int newValue;

        #endregion

        #endregion

        private void Awake()
        {
            _zoneData = GetTurretData();
            ammoStackController = IdleSignals.Instance.onGetAmmoStackController?.Invoke();
        }

        private void Start()
        {
            turretDepot = manager.OnGetAmmoDepotTarget();
            _ammoDistance = _zoneData.AmmoCountX * _zoneData.AmmoCountZ;
            _ammoStackList = ammoStackController.StackList;
        }

        public void OnDepotAmmo(GameObject obj)
        {
            if (_ammoStackList.Count >= 1 && _ammoList.Count < _zoneData.MaxAmmoCapacity)
            {
                DepotAddAmmo(obj);
            }
        }
        

        private TurretDepotAmmoData GetTurretData() =>
            Resources.Load<CD_TurretData>("Data/CD_TurretData").Data.DepotAmmoData;

        private void DepotAddAmmo(GameObject obj)
        {
            var ammo = _ammoStackList[_ammoStackList.Count - 1];
            ammo.transform.SetParent(turretDepot.transform);
            SetAmmoPosition(ammo);
            _ammoList.Add(ammo);
            if (ammo == null) return;
            _ammoStackList.RemoveAt(_ammoStackList.Count - 1);
            _ammoStackList.TrimExcess();
        }

        private void SetAmmoPosition(GameObject obj)
        {
            _direct = _zoneData.AmmoInitPoint;
            _direct.x = _direct.x + (_ammoList.Count % _zoneData.AmmoCountX) / _zoneData.OffsetFactorX;
            _direct.y = _direct.y + (_ammoList.Count / _ammoDistance) / _zoneData.OffsetFactorY;
            _direct.z = _direct.z + ((_ammoList.Count / _zoneData.AmmoCountX) % _zoneData.AmmoCountZ) /
                _zoneData.OffsetFactorZ;
            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutQuad);
            obj.transform.DOLocalMove(new Vector3(_direct.x, _direct.y, _direct.z), 0.5f).SetEase(Ease.OutQuad);
        }

        public void AmmoDecreaseDepot(int value)
        {
            var ammo = _ammoList[_ammoList.Count - 1];
            newValue += value;
            if (newValue >= 4)
            {
                _ammoList.RemoveAt(_ammoList.Count - 1);
                _ammoList.TrimExcess();
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.AmmoBox.ToString(),ammo);
                newValue = 0;
            }
        }
    }
}