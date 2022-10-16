﻿using System;
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

        public TurretStates TurretStates = TurretStates.Empty;

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject turretDepot;
        [SerializeField] private GameObject playerFiringPosition;
        [SerializeField] private TurretDepotController turretDepotController;
        [SerializeField] private TurretMovementController turretMovementController;
        [SerializeField] private TurretShootController turretShootController;
        [SerializeField] private GameObject oldParent;

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
            oldParent = PlayerSignals.Instance.onGetPlayerParent?.Invoke();
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
            PlayerSignals.Instance.onPlayerOnTurret += OnPlayerOnTurret;
            PlayerSignals.Instance.onPlayerOutTurret += OnPlayerOutTurret;
            PlayerSignals.Instance.onPlayerReadyForShoot += OnPlayerReadyForShoot;
            PlayerSignals.Instance.onGetDepotAmmoBox += OnGetDepotAmmoBox;
            InputSignals.Instance.onInputDragged += OnInputDragged;
        }



        private void UnsubscribeEvents()
        {
            IdleSignals.Instance.onGetAmmoDepotTarget -= OnGetAmmoDepotTarget;
            IdleSignals.Instance.onPlayerEnterTurretDepot -= OnPlayerEnterAmmoDepot;
            PlayerSignals.Instance.onPlayerOnTurret -= OnPlayerOnTurret;
            PlayerSignals.Instance.onPlayerOutTurret -= OnPlayerOutTurret;
            PlayerSignals.Instance.onPlayerReadyForShoot -= OnPlayerReadyForShoot;
            PlayerSignals.Instance.onGetDepotAmmoBox -= OnGetDepotAmmoBox;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
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

        private List<GameObject> OnGetDepotAmmoBox()
        {
            return turretDepotController._ammoList;
        }

        private void OnPlayerOnTurret(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            if (TurretStates == TurretStates.Empty)
            {
                TurretStates = TurretStates.PlayerOnTurret;
                player.transform.position = playerFiringPosition.transform.position;
                player.transform.SetParent(playerFiringPosition.transform);
                _rb.constraints = RigidbodyConstraints.FreezePosition;
            }

        }
        private void OnPlayerReadyForShoot(GameObject player)
        {
            if (TurretStates == TurretStates.PlayerOnTurret)
            {
                turretShootController.TurretShoot();
            }
            
        }

        public void OnPlayerOutTurret(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            if (TurretStates == TurretStates.PlayerOnTurret)
            {
                TurretStates = TurretStates.Empty;
                _rb.constraints = ~RigidbodyConstraints.FreezePosition;
                player.transform.SetParent(oldParent.transform);
                gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            }
        }

        public GameObject OnGetAmmoDepotTarget()
        {
            return turretDepot;
        }

        private void OnInputDragged(InputParams data)
        {
            turretMovementController.TurretRotation(data);
        }
    }
}