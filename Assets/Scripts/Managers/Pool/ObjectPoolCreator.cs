using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Enums;
using ObjectPool;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class ObjectPoolCreator : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private Transform _objTransformCache;
        [ShowInInspector]private CD_Pool _poolData;
        private int listCache;
        private List<GameObject> _poolGroup = new List<GameObject>();

        #endregion

        #endregion

        private void Awake()
        {
            _poolData = Resources.Load<CD_Pool>("Data/CD_Pool");
            CreatGameObjectGroup();
            InitPool();
        }

        private void CreatGameObjectGroup()
        {
            foreach (var gameObjectCache in _poolData.PoolValueDatas.Select(VARIABLE => new GameObject
                     {
                         name = VARIABLE.ObjectType.ToString(),
                         transform =
                         {
                             parent = transform
                         }
                     }))
            {
                _poolGroup.Add(gameObjectCache);
            }
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject += OnReleasePoolObject;
        }

        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject -= OnReleasePoolObject;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            listCache = (int)poolType;
            var obj = ObjectPoolManager.Instance.GetObject<GameObject>(poolType.ToString());
            return obj;
        }

        private void OnReleasePoolObject(PoolType poolType, GameObject obj)
        {
            ObjectPoolManager.Instance.ReturnObject(obj,poolType.ToString());
        }
        
        #region Pool Initialization
        
        private void InitPool()
        {
            for (int i = 0; i < _poolData.PoolValueDatas.Count; i++)
            {
                listCache = i;
                ObjectPoolManager.Instance.AddObjectPool<GameObject>(FabricateGameObject, TurnOnGameObject, TurnOffGameObject,
                    _poolData.PoolValueDatas[i].ObjectType.ToString(), _poolData.PoolValueDatas[i].ObjectLimit, true);
            }
        }
        
        private void TurnOnGameObject(GameObject gameObject)
        {
            gameObject.transform.position = _objTransformCache.position;
            gameObject.SetActive(true);
        }
        
        private void TurnOffGameObject(GameObject gameObject)
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
        
        private GameObject FabricateGameObject()
        {
            return Instantiate(_poolData.PoolValueDatas[listCache].PooledObject,Vector3.zero,
                Quaternion.identity,_poolGroup[listCache].transform);
        }
        
        #endregion
    }
}
