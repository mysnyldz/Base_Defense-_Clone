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

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject moneyTakenPoint;

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

        public void AddStack(GameObject obj)
        {
            StackList.Capacity = _data.MaxMoneyCount;
            if (_maxMoneyCount < StackList.Capacity)
            {
                obj.transform.SetParent(transform);
                ObjPosition(obj);
                StackList.Add(obj);
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

        public GameObject DecreaseStack()
        {
            if (StackList.Count >= 1)
            {
                int limit = StackList.Count;
                for (int i = 0; i <= limit; i++)
                {
                    var obj = StackList[0];
                    StackList.RemoveAt(0);
                    StackList.TrimExcess();
                    obj.transform
                        .DOLocalMove(new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f)),
                            0.5f).OnComplete(() => { obj.transform.parent = moneyTakenPoint.transform; });
                    obj.transform.DOLocalMove(new Vector3(0, 0.05f, 0), 2f).SetDelay(1f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Money.ToString(), obj);
                    });
                    CurrencySignals.Instance.onAddMoney?.Invoke(1);
                    _maxMoneyCount--;
                }

            }
            return null;
        }


    }
}