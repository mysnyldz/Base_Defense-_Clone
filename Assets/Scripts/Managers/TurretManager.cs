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
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public TurretStates TurretStates;

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject turretDepot;
        [SerializeField] private GameObject AiCollider;
        [SerializeField] private GameObject playerFiringPosition;
        [SerializeField] private TurretDepotController turretDepotController;
        [SerializeField] private TurretMovementController turretMovementController;
        [SerializeField] private TurretShootController turretShootController;
        [SerializeField] private GameObject turretOperator;
        [SerializeField] private TextMeshPro turretOperatorText;
        [SerializeField] private GameObject oldParent;

        #endregion

        #region Private Variables

        private TurretData _data;
        private Rigidbody _rb;
        private Collider _collider;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            _data = GetTurretData();
            oldParent = PlayerSignals.Instance.onGetPlayerParent?.Invoke();
            turretOperatorText.text = "Operator Area : \t Off";
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
            PlayerSignals.Instance.onDecreaseBullet += OnDecreaseBullet;
            PlayerSignals.Instance.onAiTurretArea += OnAiTurretArea;
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
            PlayerSignals.Instance.onDecreaseBullet -= OnDecreaseBullet;
            PlayerSignals.Instance.onAiTurretArea -= OnAiTurretArea;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlayerEnterAmmoDepot(GameObject depot)
        {
            if (depot == turretDepot)
            {
                turretDepotController.OnDepotAmmo(depot);
            }
        }

        private List<GameObject> OnGetDepotAmmoBox()
        {
            return turretDepotController._ammoList;
        }

        private void OnPlayerOnTurret(GameObject player, GameObject turret)
        {
            if (turret == gameObject)
            {
                _rb = player.GetComponent<Rigidbody>();
                if (TurretStates == TurretStates.Empty)
                {
                    TurretStates = TurretStates.PlayerOnTurret;
                    player.transform.position = playerFiringPosition.transform.position;
                    player.transform.SetParent(playerFiringPosition.transform);
                    _rb.constraints = RigidbodyConstraints.FreezePosition;
                    CoreGameSignals.Instance.onEnterTurret?.Invoke();
                }
            }
        }


        private void OnPlayerReadyForShoot(GameObject player)
        {
            if (TurretStates == TurretStates.PlayerOnTurret)
            {
                turretShootController.TurretShoot();
            }
        }

        private void OnAiTurretArea(GameObject obj)
        {
            if (obj == AiCollider)
            {
                _collider = gameObject.GetComponent<Collider>();
                if (TurretStates == TurretStates.Empty)
                {
                    turretOperatorText.text = "Operator: \t On";
                    TurretStates = TurretStates.AiOnTurret;
                    turretOperator.SetActive(true);
                    _collider.enabled = false;
                }
                else if (TurretStates == TurretStates.AiOnTurret)
                {
                    turretOperatorText.text = "Operator: \t Off";
                    TurretStates = TurretStates.Empty;
                    turretOperator.SetActive(false);
                    _collider.enabled = true;
                }
            }
        }


        public void AiReadyForShoot()
        {
            turretMovementController.AITurretRotation();
            turretShootController.TurretShoot();
        }

        public void OnPlayerOutTurret(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            if (TurretStates == TurretStates.PlayerOnTurret)
            {
                TurretStates = TurretStates.Empty;
                _rb.constraints = ~RigidbodyConstraints.FreezePosition;
                player.transform.SetParent(oldParent.transform);
                CoreGameSignals.Instance.onExitTurret?.Invoke();
                gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            }
        }

        private void OnDecreaseBullet(int value, GameObject obj)
        {
            turretDepotController.AmmoDecreaseDepot(1,obj);
        }

        public GameObject OnGetAmmoDepotTarget()
        {
            return turretDepot;
        }

        private void OnInputDragged(InputParams data)
        {
            turretMovementController.PlayerTurretRotation(data);
        }
    }
}