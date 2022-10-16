using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
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

        [SerializeField] private GameObject FirePoint;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _ammolist;
        private BulletTypes bulletTypes = BulletTypes.Turret;
        private Rigidbody _rb;
        private BulletTypesData _data;
        private bool _isDeath;
        private float _fireRate;
        private float _timer;

        #endregion

        #endregion

        private void Start()
        {
            _ammolist = PlayerSignals.Instance.onGetDepotAmmoBox?.Invoke();
            _data = GetFireRateData();
            _fireRate = _data.FireRate;
            
        }

        private BulletTypesData GetFireRateData() => Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[bulletTypes];

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
            bullet.transform.position = FirePoint.transform.position;
            bullet.transform.rotation = FirePoint.transform.rotation;
            
        }

        public void TurretShoot()
        {
            if (Targets.Count >= 1)
            {
                _timer += Time.deltaTime * (_fireRate);
                if (_timer >= _fireRate && _ammolist.Count >=1)
                {
                    var bullet = PoolSignals.Instance.onGetPoolObject(PoolType.TurretBullet.ToString(), transform);
                    _timer = 0;
                    BulletPosition(bullet);
                    _rb = bullet.GetComponent<Rigidbody>();
                    _rb.AddForce(FirePoint.transform.forward * 5f, ForceMode.VelocityChange);
                    
                }
            }
        }
    }
}