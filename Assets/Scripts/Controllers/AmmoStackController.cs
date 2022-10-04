using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Controllers
{
    public class AmmoStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> StackList = new List<GameObject>();

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject ammoArea;

        #endregion

        #region Private Variables

        private AmmoStackData _data;
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

        private void AddStack(GameObject obj)
        {
            
            obj.transform.SetParent(transform);
            ObjPosition(obj);
            StackList.Add(obj);
        }

        public void OnGetAmmo()
        {
            StackList.Capacity = _data.MaxAmmoCount;
            if (_maxAmmoCount < StackList.Capacity)
            {
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Ammo.ToString(), transform);
                AddStack(obj);
            }
        }

        public GameObject DecreaseStack()
        {
            if (StackList.Count > 0)
            {
                int limit = StackList.Count;
                for (int i = 0; i < limit; i++)
                {
                    var obj = StackList[0];
                    StackList.RemoveAt(0);
                    StackList.TrimExcess();
                    obj.transform
                        .DOLocalMove(new Vector3(Random.Range(-1f, 1f), Random.Range(0.1f, 1f), Random.Range(-1f, 1f)),
                            0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                        {
                            obj.transform.parent = ammoArea.transform;
                        });
                    obj.transform
                        .DOLocalRotate(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)),
                            1f).SetEase(Ease.OutBounce).SetDelay(0.5f);
                    obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 1f).SetDelay(1.5f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Ammo.ToString(), obj);
                        obj.transform.DOLocalRotate(Vector3.zero, 0f);
                        obj.transform.DOLocalMove(Vector3.zero, 0f);
                    });
                    _maxAmmoCount--;
                }
            }

            return null;
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