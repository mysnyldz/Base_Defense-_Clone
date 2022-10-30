using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        

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
                {
                    manager.MoneyAddStack(other.gameObject);
                    _maxMoneyCount++;
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
                    manager.AmmoSendAmmoWareHouse();
                    _maxAmmoCount = 0;
                }
            }

            if (other.CompareTag("GateOutside"))
            {
                manager.playerSphere.transform.DOScale(new Vector3(3f, 3f, 3f), 1f).SetEase(Ease.OutFlash);
                manager.ChangeState(PlayerStateTypes.Battle);
            }

            if (other.CompareTag("GateInside"))
            {
                manager.playerSphere.transform.DOScale(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutFlash);
                manager.Target = null;
                manager.ChangeState(PlayerStateTypes.Idle);
            }

            if (other.CompareTag("GemDepot"))
            {
                IdleSignals.Instance.onPlayerEnterGemDepot?.Invoke(gameObject);
            }

            if (other.CompareTag("Turret"))
            {
                manager.ChangeState(PlayerStateTypes.Turret);
                PlayerSignals.Instance.onPlayerOnTurret?.Invoke(manager.gameObject,other.gameObject);
            }

            if (other.CompareTag("TurretOperatorArea"))
            {
                PlayerSignals.Instance.onAiTurretArea?.Invoke(other.gameObject);
                //other.gameObject.SetActive(false);
            }

            if (other.CompareTag("Enemy"))
            {
                manager.ChangeState(PlayerStateTypes.Battle);
            }

            if (other.CompareTag("MoneySupporterBuyArea"))
            {
                IdleSignals.Instance.onMoneySupporterBuyArea?.Invoke(1);
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
                if (_timer >= _spendTime && _maxAmmoCount < manager.AmmoStackData.MaxAmmoCount)
                {
                    _timer = 0;
                    manager.AmmoAddStack();
                    _maxAmmoCount++;
                }
            }

            if (other.CompareTag("TurretDepot"))
            {
                _timer += (Time.fixedDeltaTime) * 2.5f;
                if (_timer >= _spendTime)
                {
                    IdleSignals.Instance.onPlayerEnterTurretDepot?.Invoke(other.gameObject);
                    manager.AmmoDecreaseStack();
                    _maxAmmoCount--;
                    _timer = 0;
                }
            }
        }
    }
}