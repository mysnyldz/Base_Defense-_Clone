using System;
using System.Collections.Generic;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject turretDepot;
        [SerializeField] private List<GameObject> _ammoList = new List<GameObject>();
        [SerializeField] private List<GameObject> _ammoStackList = new List<GameObject>();
        [SerializeField] private AmmoStackController ammoStackController;

        #endregion

        #region Private Variables

        private TurretAmmoData _zoneData;
        private List<int> _capacity;
        private int _ammoDistance;
        private Vector3 _direct = Vector3.zero;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            _zoneData = GetTurretData();
            ammoStackController = IdleSignals.Instance.onGetAmmoStackController?.Invoke();
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onGetAmmoDepotTarget += OnGetAmmoDepotTarget;
            IdleSignals.Instance.onPlayerEnterTurretDepot += OnPlayerEnterAmmoDepot;
        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onGetAmmoDepotTarget -= OnGetAmmoDepotTarget;
            IdleSignals.Instance.onPlayerEnterTurretDepot -= OnPlayerEnterAmmoDepot;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _ammoDistance = _zoneData.AmmoCountX * _zoneData.AmmoCountZ;
            _ammoStackList = ammoStackController.StackList;
        }

        private TurretAmmoData GetTurretData() => Resources.Load<CD_TurretAmmoData>("Data/CD_TurretAmmoData").Data;


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

        private GameObject OnGetAmmoDepotTarget()
        {
            return turretDepot;
        }


        public GameObject AmmoDecreaseDepot()
        {
            return null;
        }

        private void OnPlayerEnterAmmoDepot(GameObject obj)
        {
            DepotAddAmmo(obj);
        }

        private void SetAmmoPosition(GameObject obj)
        {
            _direct = _zoneData.AmmoInitPoint;
            _direct.x = _direct.x + (_ammoList.Count % _zoneData.AmmoCountX) / _zoneData.OffsetFactorX;
            _direct.y = _direct.y + (_ammoList.Count / _ammoDistance)  / _zoneData.OffsetFactorY;
            _direct.z = _direct.z + ((_ammoList.Count / _zoneData.AmmoCountX) % _zoneData.AmmoCountZ) /_zoneData.OffsetFactorZ ;
            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutQuad);
            obj.transform.DOLocalMove(new Vector3(_direct.x, _direct.y, _direct.z), 0.5f).SetEase(Ease.OutQuad);
        }
    }
}