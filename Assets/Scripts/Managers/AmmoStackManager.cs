using System;
using System.Collections.Generic;
using Controllers;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AmmoStackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        

        #endregion

        #region Serialized Variables

        [SerializeField] private AmmoStackController ammoStackController;

        #endregion

        #region Private Variables
        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleSignals.Instance.onGetAmmo += OnGetAmmo;
            IdleSignals.Instance.onGetAmmoController += OnGetAmmoController;
            IdleSignals.Instance.onGetAmmoManager += OnGetAmmoManager;
        }



        private void UnsubscribeEvent()
        {
            IdleSignals.Instance.onGetAmmo -= OnGetAmmo;
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        #endregion
        
        public void AddStack(GameObject obj)
        {
            ammoStackController.AddStack(obj);
        }

        private GameObject OnGetAmmo()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Ammo,transform);
            return obj;
        }
        private GameObject OnGetAmmoManager()
        {
            return gameObject;
        }

        private GameObject OnGetAmmoController()
        {
            return ammoStackController.gameObject;
        }

    }
}