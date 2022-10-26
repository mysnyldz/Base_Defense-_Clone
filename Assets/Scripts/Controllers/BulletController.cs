using System;
using System.Threading.Tasks;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables



        #endregion

        #region Serializefield Variables

        [SerializeField] private BulletTypes bulletTypes;
        [SerializeField] private Rigidbody rb;
        
        #endregion

        #region Private Variables

        private BulletTypesData _data;
        private int _damage;
        private int _fireRate;
        private int _enemyHealth;
        private EnemyTypes _enemyTypes;
        #endregion

        #endregion
        
        private void OnDisable()
        {
            rb.velocity = Vector3.zero;
        }

        private void Start()
        {
            _data = Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[bulletTypes];
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.TurretBullet.ToString(), gameObject);
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.PistolBullet.ToString(), gameObject);
                EnemySignals.Instance.onTakeDamage?.Invoke((int)_data.Damage,other.gameObject);
            }
        }

        private async void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("TurretRange"))
            {
                await Task.Delay(600);
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.TurretBullet.ToString(), gameObject);
            }
            if (other.CompareTag("PlayerSphere"))
            {
                await Task.Delay(600);
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.PistolBullet.ToString(), gameObject);
            }
        }
        
    }
}