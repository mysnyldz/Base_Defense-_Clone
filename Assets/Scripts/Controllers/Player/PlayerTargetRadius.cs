using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using States.EnemyStates;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerTargetRadius : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> TargetList = new List<GameObject>();

        #endregion

        #region Serializefield Variables

        [SerializeField] private PlayerManager playerManager;
        [ShowInInspector] private BulletTypesData _data;

        #endregion

        #region Private Variables

        private float _damage;
        private float _fireRate;
        private float _bulletSpeed;
        private BulletTypes _types = BulletTypes.Pistol;
        protected Coroutine AttackCoroutine;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBulletData();
        }

        private BulletTypesData GetBulletData() =>
            Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[_types];

        protected virtual void Start()
        {
            _damage = _data.Damage;
            _fireRate = _data.FireRate;
            _bulletSpeed = _data.BulletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnAddTargetList(other.gameObject);
                if (AttackCoroutine == null)
                {
                    AttackCoroutine = StartCoroutine(Attack());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnRemoveTargetList(other.gameObject);
                Debug.Log("girdi");
            }
        }

        public void OnAddTargetList(GameObject obj)
        {
            TargetList.Add(obj);
        }

        public void OnRemoveTargetList(GameObject obj)
        {
            if (TargetList.Count > 0)
            {
                if (TargetList.IndexOf(obj) == 0)
                {
                    Debug.Log("target index: " + TargetList.IndexOf(obj));
                    playerManager.Target = null;
                }

                TargetList.Remove(obj);
                TargetList.TrimExcess();
                if (TargetList.Count == 0)
                {
                    StopCoroutine(AttackCoroutine);
                    AttackCoroutine = null;
                }

                SetTarget();
            }
        }

        private void SetTarget()
        {
            if (TargetList.Count >= 1)
            {
                playerManager.Target = TargetList[0];
            }
        }

        IEnumerator Attack()
        {
            WaitForSeconds waiter = new WaitForSeconds(_fireRate);

            while (TargetList.Count >= 1)
            {
                Debug.Log("while");
                if (playerManager.Target == null)
                {
                    SetTarget();
                }

                if(playerManager.Target != null)
                {
                    Debug.Log("Ates");
                    Fire();
                }

                yield return waiter;
                playerManager.Target = null;
                StopFire();
            }

            playerManager.Target = null;
        }

        protected virtual void Fire()
        {
        }

        protected virtual void StopFire()
        {
        }
    }
}