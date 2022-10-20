using System;
using DG.Tweening;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MineExplosionManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private int minePrice;
        [SerializeField] private SphereCollider mineCollider;
        [SerializeField] private TextMeshPro textMesh;
        [ShowInInspector] private GameObject playerManager;

        #endregion

        #region Private Variables

        private float _buyTimer;
        private float _waitToEnabledMine = 50;
        [ShowInInspector]private MineExplosionTypes _mineExplosionTypes = MineExplosionTypes.Uncomplete;
        private int _gem;

        #endregion

        #endregion

         #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerOnMineExplosion += OnPlayerOnMineExplosion;
            PlayerSignals.Instance.onPlayerOffMineExplosion += OnPlayerOfMineExplosion;
        }



        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerOnMineExplosion -= OnPlayerOnMineExplosion;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Start()
        {
            mineCollider.transform.localScale = Vector3.zero;
        }

        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        StateControll(_mineExplosionTypes);
        //    }
        //}
        
        private void OnPlayerOnMineExplosion(GameObject player)
        {
            playerManager = player;
            if (playerManager != null)
            {
                StateControll(MineExplosionTypes.Uncomplete);
            }
        }
        private void OnPlayerOfMineExplosion(GameObject player)
        {
            playerManager = null;
        }

        private void StateControll(MineExplosionTypes types)
        {
            switch (types)
            {
                case MineExplosionTypes.Uncomplete:
                    BuyMine();
                    SetMineText();
                    break;
                case MineExplosionTypes.Complete:
                    WaitToEnabledMine();
                    SetMineText();
                    break;
            }
        }

        private void WaitToEnabledMine()
        {
            _waitToEnabledMine -= Time.deltaTime;
            SetMineText();
            if (_waitToEnabledMine <= 0)
            {
                _mineExplosionTypes = MineExplosionTypes.Uncomplete;
                mineCollider.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetEase(Ease.OutFlash);
                _waitToEnabledMine = 50;
            }
        }

        private void BuyMine()
        {
            _buyTimer += (Time.fixedDeltaTime) * 20;
            if (_buyTimer >= 2f)
            {
                _gem = CurrencySignals.Instance.onGetGem.Invoke();
                if (_mineExplosionTypes == MineExplosionTypes.Uncomplete)
                {
                    if (_gem >= minePrice)
                    {
                        CurrencySignals.Instance.onReduceGem?.Invoke(1);
                        minePrice -= 1;
                        SetMineText();
                        if (minePrice <= 0)
                        {
                            mineCollider.transform.DOScale(new Vector3(15f, 15f, 15f), 1f).SetEase(Ease.OutFlash);
                            _mineExplosionTypes = MineExplosionTypes.Complete;
                        }
                    }
                }

                _buyTimer = 0;
            }
        }

        private void SetMineText()
        {
            if (_mineExplosionTypes == MineExplosionTypes.Uncomplete)
            {
                textMesh.text = minePrice.ToString();
            }
            else if(_mineExplosionTypes == MineExplosionTypes.Complete)
            {
                var localTimer = (int)_waitToEnabledMine;
                textMesh.text = localTimer.ToString();
            }
        }
    }
}