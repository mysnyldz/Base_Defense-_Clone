using System;
using System.Collections.Generic;
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

        public List<GameObject> EnemyList = new List<GameObject>();

        #endregion

        #region Serializefield Variables

        [SerializeField] private int minePrice;
        [SerializeField] private SphereCollider mineCollider;
        [SerializeField] private TextMeshPro textMesh;
        [SerializeField] private TextMeshPro buyTextMesh;
        [SerializeField] private GameObject buyZone;
        [SerializeField] private GameObject explosionZone;

        #endregion

        #region Private Variables

        private float _buyTimer;
        private float _enabledMineTimer = 50;
        private float _explosionTimer = 10;

        [ShowInInspector] private MineExplosionTypes _mineExplosionTypes = MineExplosionTypes.Uncomplete;
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
        }


        private void UnsubscribeEvents()
        {
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

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_mineExplosionTypes == MineExplosionTypes.Uncomplete)
                {
                    BuyMine();
                    SetMineText();
                }
            }
        }

        private void Update()
        {
            if (_mineExplosionTypes == MineExplosionTypes.Explossion)
            {
                Explosion();
                SetMineText();
            }
            else if (_mineExplosionTypes == MineExplosionTypes.Complete)
            {
                WaitToEnabledMine();
                SetMineText();
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyList.Add(other.gameObject);
            }
        }

        private void WaitToEnabledMine()
        {
            _enabledMineTimer -= Time.deltaTime;
            if (_enabledMineTimer <= 0)
            {
                _mineExplosionTypes = MineExplosionTypes.Uncomplete;
                buyZone.SetActive(true);
                minePrice = 10;
                _enabledMineTimer = 50;
                textMesh.text = "";
            }
            
        }

        private void Explosion()
        {
            _explosionTimer -= Time.deltaTime;
            if (_explosionTimer <= 0)
            {
                for (int i = 0; i < EnemyList.Count - 1; i++)
                {
                    EnemySignals.Instance.onTakeDamage?.Invoke(50, EnemyList[0]);
                    EnemyList.RemoveAt(0);
                    EnemyList.TrimExcess();
                }
                mineCollider.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetEase(Ease.OutFlash);
                explosionZone.transform.DOScale(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.Flash);
                _explosionTimer = 10;
                _mineExplosionTypes = MineExplosionTypes.Complete;
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
                            mineCollider.transform.DOScale(new Vector3(7f, 7f, 7f), 1f).SetEase(Ease.OutFlash);
                            explosionZone.transform.DOScale(new Vector3(1.35f, 1.35f, 1), 1f).SetEase(Ease.Flash);
                            _mineExplosionTypes = MineExplosionTypes.Explossion;
                            buyZone.SetActive(false);
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
                buyTextMesh.text = minePrice.ToString();
            }
            else if (_mineExplosionTypes == MineExplosionTypes.Complete)
            {
                textMesh.text = ((int)_enabledMineTimer).ToString();
            }
            else if (_mineExplosionTypes == MineExplosionTypes.Explossion)
            {
                textMesh.text = ((int)_explosionTimer).ToString();
            }
        }
    }
}