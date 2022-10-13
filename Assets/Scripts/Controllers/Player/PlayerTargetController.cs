using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using States.EnemyStates;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerTargetController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> TargetList = new List<GameObject>();
        public int Damage;
        public int FireRate;

        #endregion

        #region Serializefield Variables

        #endregion

        #region Private Variables

        private GameObject _enemy;
        private BulletTypesData _data;
        private BulletTypes _types;
        private Coroutine AttackCorotuine;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBulletData();
        }

        private BulletTypesData GetBulletData() =>
            Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[_types];

        private void Start()
        {
            Damage = _data.Damage;
            FireRate = _data.FireRate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnAddTargetList(other.gameObject);
                if (AttackCorotuine == null)
                {
                    AttackCorotuine = StartCoroutine(Attack());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnRemoveTargetList(other.gameObject);
                if (TargetList.Count == 0)
                {
                    StopFire();
                    StopCoroutine(AttackCorotuine);
                    AttackCorotuine = null;
                }
            }
        }


        public void OnAddTargetList(GameObject obj)
        {
            TargetList.Add(obj);
        }

        public void OnRemoveTargetList(GameObject obj)
        {
            TargetList.Remove(obj);
            TargetList.TrimExcess();
        }

        private IEnumerator Attack()
        {
            WaitForSeconds Wait = new WaitForSeconds(FireRate);
            yield return Wait;

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
                        _enemy = TargetList[i];
                    }
                }

                if (_enemy != null)
                {
                    Fire();
                }
                
                _enemy = null;
                closestdistance = float.MaxValue;
                yield return Wait;
                
                
            }
            
        }

        protected virtual void Fire()
        {
        }

        protected virtual void StopFire()
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