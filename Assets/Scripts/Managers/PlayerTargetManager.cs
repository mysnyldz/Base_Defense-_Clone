using System.Collections;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.GettingStarted;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Managers
{
    public class PlayerTargetManager : PlayerTargetRadius
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private GameObject bulletFirePoint;

        [SerializeField] private GameObject pistol;
        //[SerializeField] private GameObject machineGun;
        //[SerializeField] private GameObject shootGun;

        #endregion

        #region Private Variables

        private BulletTypesData _data;
        private GameObject _enemy;
        private BulletTypes _bulletTypes = BulletTypes.Pistol;
        private Rigidbody _rb;

        #endregion

        #endregion


        #region Event Subscription

        // private void OnEnable()
        // {
        //     SubscribeEvents();
        // }

        // private void SubscribeEvents()
        // {
        // }

        // private void UnsubscribeEvents()
        // {
        // }

        // private void OnDisable()
        // {
        //     UnsubscribeEvents();
        // }

        #endregion

        protected override void Start()
        {
            GetReferences();
            base.Start();
        }

        private void GetReferences()
        {
            _data = GetBulletData();
        }

        private BulletTypesData GetBulletData() =>
            Resources.Load<CD_BulletData>("Data/CD_BulletData").Data.BulletTypeDatas[_bulletTypes];


        public void PlayerInBase()
        {
            if (manager.BattleMode) return;
            pistol.SetActive(false);
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
                TargetList.Clear();
            }
        }

        public void PlayerInBattle()
        {
            if (manager.IdleMode) return;
            pistol.SetActive(true);
        }

        public void PlayerIsDie()
        {
            if (manager.BattleMode) return;
            pistol.SetActive(false);
            manager.Target = null;
            manager.playerSphere.transform.DOScale(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutFlash);
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
                TargetList.Clear();
            }
        }
        

        public void SetPlayerTargetStateTypes(PlayerStateTypes types)
        {
            switch (types)
            {
                case PlayerStateTypes.Idle:
                    PlayerInBase();
                    break;
                case PlayerStateTypes.Battle:
                    PlayerInBattle();
                    break;
                case PlayerStateTypes.Death:
                    PlayerIsDie();
                    break;
                    
            }
        }

        protected override void Fire()
        {
            var bullet =
                PoolSignals.Instance.onGetPoolObject(PoolType.PistolBullet.ToString(), bulletFirePoint.transform);
            _rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.position = bulletFirePoint.transform.position;
            bullet.transform.rotation = bulletFirePoint.transform.rotation;
            _rb.AddForce(bulletFirePoint.transform.forward * _data.BulletSpeed, ForceMode.VelocityChange);
        }

        protected override void StopFire()
        {
            PlayerSignals.Instance.onTargetInSight?.Invoke(false);
        }
    }
}