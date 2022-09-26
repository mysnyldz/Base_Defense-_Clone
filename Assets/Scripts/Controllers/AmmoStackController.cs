﻿using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class AmmoStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> StackList = new List<GameObject>();

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private AmmoStackData _data;
        private int _currentStackLevel;
        private float _directx;
        private float _directy;
        private float _directz;
        private int _maxAmmoCount = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _data = Resources.Load<CD_AmmoStackData>("Data/CD_AmmoStackData").Data;
        }

        public void AddStack(GameObject obj)
        {
            StackList.Capacity = _data.MaxAmmoCount;
            if (_maxAmmoCount < StackList.Capacity)
            {
                obj.transform.SetParent(transform);
                ObjPosition(obj);
                StackList.Add(obj);
            }
            else
            {
                return;
            }
        }
        public void OnGetAmmo()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Ammo,transform);
            AddStack(obj);
        }

        private void ObjPosition(GameObject obj)
        {
            _directx = 0;
            _directy = StackList.Count % _data.AmmoCountY * _data.OffsetFactorY;
            _directz = -(StackList.Count % (_data.AmmoCountZ * _data.AmmoCountY) / _data.AmmoCountY *
                         _data.OffsetFactorZ);
            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutQuad);
            obj.transform.DOLocalMove(new Vector3(_directx, _directy, _directz), 0.5f).SetEase(Ease.OutQuad);
            _maxAmmoCount++;
        }
    }
}