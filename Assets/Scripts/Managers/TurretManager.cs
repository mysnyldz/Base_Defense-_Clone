using System;
using System.Collections.Generic;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
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
        [SerializeField] private GameObject playerFiringPosition;
        [SerializeField] private TurretDepotController turretDepotController;
        [SerializeField] private TurretStates TurretStates = TurretStates.Empty;
        [SerializeField] private GameObject playerMovement;

        #endregion

        #region Private Variables

        private TurretData _data;
        private Rigidbody _rb;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            _data = GetTurretData();
            playerMovement = IdleSignals.Instance.onPlayerMovement?.Invoke();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onGetAmmoDepotTarget += OnGetAmmoDepotTarget;
            IdleSignals.Instance.onPlayerEnterTurretDepot += OnPlayerEnterAmmoDepot;
            IdleSignals.Instance.onPlayerOnTurret += OnPlayerOnTurret;

        }


        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onGetAmmoDepotTarget -= OnGetAmmoDepotTarget;
            IdleSignals.Instance.onPlayerEnterTurretDepot -= OnPlayerEnterAmmoDepot;
            IdleSignals.Instance.onPlayerOnTurret -= OnPlayerOnTurret;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlayerEnterAmmoDepot(GameObject obj)
        {
            turretDepotController.OnDepotAmmo(obj);
        }

        private void OnPlayerOnTurret(GameObject obj)
        {
            _rb = obj.GetComponent<Rigidbody>();
            if (TurretStates == TurretStates.Empty)
            {
                TurretStates = TurretStates.PlayerOnTurret;
                obj.transform.SetParent(playerFiringPosition.transform);
                obj.transform.position = playerFiringPosition.transform.position;
                InputSignals.Instance.onInputReleased?.Invoke();
                _rb.constraints = RigidbodyConstraints.FreezePosition;
            }


        }

        public GameObject OnGetAmmoDepotTarget()
        {
            return turretDepot;
        }
        
        
    }
}