using System;
using System.Collections;
using System.Collections.Generic;
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
        [ShowInInspector] private BulletData _data;

        #endregion

        #region Private Variables

        private float _damage;
        private float _fireRate;
        private BulletTypes _types;
        protected Coroutine AttackCoroutine;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBulletData();
        }

        private BulletData GetBulletData() => Resources.Load<CD_BulletData>("Data/CD_BulletData").Data;

        protected virtual void Start()
        {
            _damage = _data.BulletTypeDatas[BulletTypes.Pistol].Damage;
            _fireRate = _data.BulletTypeDatas[BulletTypes.Pistol].FireRate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnAddTargetList(other.gameObject);
                if (TargetList.Count >= 1 && AttackCoroutine == null)
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
            }
        }

        public void OnAddTargetList(GameObject obj)
        {
            TargetList.Add(obj);
        }

        public void OnRemoveTargetList(GameObject obj)
        {
            if (TargetList[0] == obj)
            {
                playerManager.Target = null;
                StopFire();
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
                TargetList.Remove(obj);
            }

            TargetList.Remove(obj);
            TargetList.TrimExcess();
        }

        IEnumerator Attack()
        {
            WaitForSeconds waiter = new WaitForSeconds(_fireRate);
            float closestdistance = float.MaxValue;

            while (TargetList.Count >= 1)
            {
                for (int i = 0; i < TargetList.Count; i++)
                {
                    var enemyTransform = TargetList[i].transform;
                    var distance = Vector3.Distance(transform.position, enemyTransform.position);
                    if (distance < closestdistance)
                    {
                        closestdistance = distance;
                        playerManager.Target = TargetList[i];
                    }
                }

                if (playerManager.Target == null)
                {
                    playerManager.Target = TargetList[0];
                }

                TargetInSight();
                if (playerManager.Target != null)
                {
                    Fire();
                }

                yield return waiter;
                playerManager.Target = null;
                closestdistance = float.MaxValue;
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

        protected virtual void TargetInSight()
        {
        }

        protected virtual bool TriggerEnter(Collider other)
        {
            return false;
        }

        protected virtual bool TriggerExit(Collider other)
        {
            return false;
        }
    }
}