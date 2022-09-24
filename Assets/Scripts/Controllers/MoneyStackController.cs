using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MoneyStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> StackList = new List<GameObject>();

        #endregion

        #region Serialized Variables
        

        #endregion

        #region Private Variables

        private MoneyStackData _data;
        private float _directx;
        private float _directy;
        private float _directz;

        #endregion

        #endregion

        private void Awake()
        {
            _data = Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;
        }

        public void AddStack(GameObject obj)
        {
            obj.transform.SetParent(transform);
            ObjPosition(obj);
            StackList.Add(obj);
        }

        private void ObjPosition(GameObject obj)
        {
            _directx = 0;
            _directy = StackList.Count % _data.MoneyCountY * _data.OffsetFactorY;
            _directz = -(StackList.Count % (_data.MoneyCountZ * _data.MoneyCountY) / _data.MoneyCountY *
                         _data.OffsetFactorZ);
            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutQuad);
            obj.transform.DOLocalMove(new Vector3(_directx, _directy, _directz), 0.5f).SetEase(Ease.OutQuad);
        }
    }
}