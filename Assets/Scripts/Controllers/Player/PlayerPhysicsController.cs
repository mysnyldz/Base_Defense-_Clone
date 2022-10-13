using System;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private GameObject playerSphere;

        #endregion

        #region Private Variables

        private int _spendTime = 1;
        private float _timer;
        private int _maxAmmoCount = 0;
        private int _maxTurretAmmoCount = 0;
        private int _maxMoneyCount = 0;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                if (_maxMoneyCount < manager.MoneyStackData.MaxMoneyCount)
                {
                    manager.MoneyAddStack(other.gameObject);
                    _maxMoneyCount++;
                    other.GetComponent<Rigidbody>().useGravity = false;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    other.GetComponent<Collider>().enabled = false;
                }
            }

            if (other.CompareTag("StackDropZone"))
            {
                if (_maxMoneyCount >= 1)
                {
                    manager.MoneyDecreaseStack();
                    _maxMoneyCount = 0;
                }

                if (_maxAmmoCount >= 1)
                {
                    manager.AmmoDecreaseStack();
                    _maxAmmoCount = 0;
                }
            }

            if (other.CompareTag("GateOutside"))
            {
                playerSphere.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 1f).SetEase(Ease.OutFlash);
                manager.SetPlayerStateTypes(PlayerStateTypes.Battle);
            }

            if (other.CompareTag("GateInside"))
            {
                playerSphere.transform.DOScale(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutFlash);
                manager.SetPlayerStateTypes(PlayerStateTypes.Idle);
            }

            if (other.CompareTag("GemDepot"))
            {
                IdleSignals.Instance.onPlayerEnterGemDepot?.Invoke(gameObject);
            }
            if (other.CompareTag("Turret"))
            {
                 manager.IsTurretState();
                 PlayerSignals.Instance.onPlayerOnTurret?.Invoke(manager.gameObject);
            }

            if (other.CompareTag("Enemy"))
            {
                manager.SetPlayerStateTypes(PlayerStateTypes.Target);
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
                PlayerSignals.Instance.onPlayerReadyForShoot?.Invoke(gameObject);
            }
            
            if (other.CompareTag("AmmoWarehouse"))
            {
                _timer += (Time.fixedDeltaTime) * 3;
                if (_timer >= _spendTime && _maxAmmoCount <= manager.AmmoStackData.MaxAmmoCount)
                {
                    _timer = 0;
                    manager.AmmoAddStack();
                    _maxAmmoCount++;
                }
            }

            // if (other.CompareTag("TurretDepot"))
            // {
            //     _timer += (Time.fixedDeltaTime) * 2.5f;
            //     if (_timer >= _spendTime && _maxTurretAmmoCount < manager.TurretData.DepotAmmoData.MaxAmmoCapacity )
            //     {
            //         _timer = 0;
            //         IdleSignals.Instance.onPlayerEnterTurretDepot?.Invoke(gameObject);
            //         _maxTurretAmmoCount++;
            //     }
            // }
        }
    }
}