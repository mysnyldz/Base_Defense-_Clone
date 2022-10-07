using System;
using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager _manager;
        [SerializeField] private GameObject _playerSphere;

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
                if (_maxMoneyCount < _manager.MoneyStackData.MaxMoneyCount)
                {
                    _manager.MoneyAddStack(other.gameObject);
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
                    _manager.MoneyDecreaseStack();
                    _maxMoneyCount = 0;
                }

                if (_maxAmmoCount >= 1)
                {
                    _manager.AmmoDecreaseStack();
                    _maxAmmoCount = 0;
                }
            }

            if (other.CompareTag("GateOutside"))
            {
                _playerSphere.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 1f).SetEase(Ease.OutFlash);
            }

            if (other.CompareTag("GateInside"))
            {
                _playerSphere.transform.DOScale(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutFlash);
            }

            if (other.CompareTag("GemDepot"))
            {
                IdleSignals.Instance.onPlayerEnterGemDepot?.Invoke(gameObject);
            }

            if (other.CompareTag("Turret"))
            {
                IdleSignals.Instance.onPlayerOnTurret?.Invoke(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoWarehouse"))
            {
                _timer += (Time.fixedDeltaTime) * 3;
                if (_timer >= _spendTime && _maxAmmoCount <= _manager.AmmoStackData.MaxAmmoCount)
                {
                    _timer = 0;
                    _manager.AmmoAddStack();
                    _maxAmmoCount++;
                }
            }

            if (other.CompareTag("TurretDepot"))
            {
                _timer += (Time.fixedDeltaTime) * 1.5f;
                if (_timer >= _spendTime && _maxTurretAmmoCount < _manager.TurretData.MaxAmmoCapacity)
                {
                    _timer = 0;
                    IdleSignals.Instance.onPlayerEnterTurretDepot?.Invoke(gameObject);
                    _maxTurretAmmoCount++;
                }
            }
        }
    }
}