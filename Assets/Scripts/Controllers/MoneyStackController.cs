using System;
using System.Collections.Generic;
using Cinemachine;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class MoneyStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> StackList = new List<GameObject>();
        public GameObject OldParent;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject moneyTakenPoint;
        [SerializeField] private MoneyStackTypes types;

        #endregion

        #region Private Variables

        private MoneyStackData _data;
        private float _directx;
        private float _directy;
        private float _directz;
        private int _maxMoneyCount = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _data = Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;
        }


        public void DeathDecreaseStack()
        {
            if (StackList.Count >= 1)
            {
                for (int i = 0; i <= StackList.Count - 1; i++)
                {
                    var obj = StackList[StackList.Count - 1];
                    var moneyRb = obj.GetComponent<Rigidbody>();
                    var moneyCollider = obj.GetComponent<Collider>();
                    StackList.RemoveAt(StackList.Count - 1);
                    StackList.TrimExcess();
                    moneyRb.useGravity = true;
                    moneyRb.isKinematic = false;
                    moneyCollider.enabled = true;
                    obj.transform.DOJump(new Vector3(obj.transform.position.x + Random.Range(-1, 1),
                            obj.transform.position.y + Random.Range(0.5f, 2),
                            obj.transform.position.z + Random.Range(-1, 1)),
                        2, 1, 0.5f).SetDelay(1f);
                    obj.transform.SetParent(OldParent.transform);
                    _maxMoneyCount--;
                }
            }
        }


        public void AddStack(GameObject obj)
        {
            OldParent = obj.gameObject.transform.parent.gameObject;
            var moneyRb = obj.GetComponent<Rigidbody>();
            var moneyCollider = obj.GetComponent<Collider>();
            switch (types)
            {
                case MoneyStackTypes.Player:
                    StackList.Capacity = _data.PlayerMaxMoneyCount;
                    if (_maxMoneyCount < StackList.Capacity)
                    {
                        moneyRb.useGravity = false;
                        moneyRb.isKinematic = true;
                        moneyCollider.enabled = false;
                        obj.transform.SetParent(transform);
                        ObjPosition(obj);
                        StackList.Add(obj);
                        IdleSignals.Instance.onRemoveMoneyList?.Invoke(obj);
                    }

                    break;
                case MoneyStackTypes.Supporter:
                    StackList.Capacity = _data.SupporterMaxMoneyCount;
                    if (_maxMoneyCount < StackList.Capacity)
                    {
                        moneyTakenPoint = IdleSignals.Instance.onMoneyGetTakenPoints?.Invoke();
                        moneyRb.useGravity = false;
                        moneyRb.isKinematic = true;
                        moneyCollider.enabled = false;
                        obj.transform.SetParent(transform);
                        ObjPosition(obj);
                        StackList.Add(obj);
                        IdleSignals.Instance.onRemoveMoneyList?.Invoke(obj);
                    }

                    break;
            }
        }

        private void ObjPosition(GameObject obj)
        {
            _directx = 0;
            _directy = StackList.Count % _data.MoneyCountY * _data.OffsetFactorY;
            _directz = -(StackList.Count % (_data.MoneyCountZ * _data.MoneyCountY) / _data.MoneyCountY *
                         _data.OffsetFactorZ);
            obj.transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad);
            obj.transform.DOLocalMove(new Vector3(_directx, _directy, _directz), 0.5f).SetEase(Ease.OutQuad);
            _maxMoneyCount++;
        }

        public void DecreaseStack()
        {
            if (StackList.Count >= 1)
            {
                int limit = StackList.Count - 1;
                for (int i = 0; i <= limit; i++)
                {
                    var obj = StackList[0];
                    var moneyRb = obj.GetComponent<Rigidbody>();
                    var moneyCollider = obj.GetComponent<Collider>();
                    StackList.RemoveAt(0);
                    StackList.TrimExcess();
                    obj.transform
                        .DOLocalMove(new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f)),
                            0.5f).OnComplete(() => { obj.transform.parent = moneyTakenPoint.transform; });
                    obj.transform.DOLocalMove(new Vector3(0, 0.05f, 0), 2f).SetDelay(1f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Money.ToString(), obj);
                        moneyRb.useGravity = true;
                        moneyRb.isKinematic = false;
                        moneyCollider.enabled = true;
                    });
                    CurrencySignals.Instance.onAddMoney?.Invoke(1);
                    _maxMoneyCount--;
                }
            }
        }
    }
}