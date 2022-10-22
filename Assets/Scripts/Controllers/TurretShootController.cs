using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class TurretShootController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> Targets;

        #endregion

        #region Serializefield Variables

        [SerializeField] private TurretManager manager;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private TurretMovementController turretMovementController;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _ammolist;
        private BulletTypes bulletTypes = BulletTypes.Turret;
        private Rigidbody _rb;
        [ShowInInspector] private BulletTypesData _data;
        private bool _isDeath;
        [ShowInInspector] private float _fireRate;
        private float _timer;

        #endregion

        #endregion

        private void Start()
        {
            _ammolist = PlayerSignals.Instance.onGetDepotAmmoBox?.Invoke();
            _data = GetFireRateData();
            _fireRate = _data.FireRate;
        }

        private BulletTypesData GetFireRateData() =>
            Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[bulletTypes];

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                TargetAddList(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                TargetRemoveList(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (other.GetComponent<EnemyManager>().Health())
                {
                    TargetRemoveList(other.gameObject);
                }

                if (manager.TurretStates == TurretStates.AiOnTurret)
                {
                    manager.AiReadyForShoot();
                }
            }
        }


        private void TargetAddList(GameObject obj)
        {
            Targets.Add(obj.gameObject);
        }

        private void TargetRemoveList(GameObject obj)
        {
            Targets.Remove(obj.gameObject);
        }


        private void BulletPosition(GameObject bullet)
        {
            var parent = bullet.transform.parent.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
        }

        public void TurretShoot()
        {
            if (Targets.Count >= 1)
            {
                _timer += Time.deltaTime * (_fireRate);
                if (_timer >= _fireRate && _ammolist.Count >= 1)
                {
                    var bullet = PoolSignals.Instance.onGetPoolObject(PoolType.TurretBullet.ToString(), transform);
                    _timer = 0;
                    BulletPosition(bullet);
                    _rb = bullet.GetComponent<Rigidbody>();
                    _rb.AddForce(firePoint.transform.forward * 7.5f, ForceMode.VelocityChange);
                    PlayerSignals.Instance.onDecreaseBullet?.Invoke(1);
                }
            }
        }
    }
}