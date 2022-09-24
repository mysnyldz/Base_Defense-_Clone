using System.Collections.Generic;
using System.Threading.Tasks;
using Command.BaseCommands;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using ValueObject;


namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BaseData")] public CD_Base BaseData;

        [Header("BaseIdData")] public BaseIdData BaseIdData;

        #endregion Public Variables

        #region Serialized Variables

        [Space] [SerializeField] private GameObject BaseHolder;

        [SerializeField] private BaseLoaderCommand baseLoader;
        [SerializeField] private ClearActiveBaseCommand baseClearer;

        [ShowInInspector] private Dictionary<int, RoomData> _roomDictionary = new Dictionary<int, RoomData>();

        #endregion Serialized Variables

        #region Private Variables

        private CD_Base _cdBase;

        private int _baseID;
        private int _uniqueID;

        #endregion Private Variables

        #endregion Self Variables

        #region MonoBehavior Methods

        private void Start()
        {
            GetData();
            OnInitializeBase();
        }

        #endregion

        private void GetData()
        {
            if (!ES3.FileExists($"Base{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Base"))
                {
                    BaseData = GetBaseData();
                    Save();
                }
            }
            Load();
            BaseData = GetBaseData();
        }

        private CD_Base GetBaseData()
        {
            return Resources.Load<CD_Base>("Data/CD_Base");
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onBaseInitialize += OnInitializeBase;
            CoreGameSignals.Instance.onClearActiveBase += OnClearActiveBase;
            CoreGameSignals.Instance.onNextBase += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onBaseInitialize -= OnInitializeBase;
            CoreGameSignals.Instance.onClearActiveBase -= OnClearActiveBase;
            CoreGameSignals.Instance.onNextBase -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSave()
        {
            Save();
        }
        
        #region Level Management

        private void OnNextLevel()
        {
            _baseID++;
            Save();
            CoreGameSignals.Instance.onReset?.Invoke();
            UISignals.Instance.onSetBaseText?.Invoke(_baseID);
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveBase?.Invoke();
            CoreGameSignals.Instance.onBaseInitialize?.Invoke();
        }

        private void OnInitializeBase()
        {
            Instantiate(
                Resources.Load<GameObject>($"Prefabs/Bases/Base {_baseID}"), BaseHolder.transform
            );
        }
        private void OnClearActiveBase()
        {
            baseClearer.ClearActiveBase(BaseHolder.transform);
        }
        #endregion

        #region Level Save and Load

        public void Save()
        {
            BaseIdData baseIdData = new BaseIdData(_baseID);

            SaveLoadSignals.Instance.onSaveIdleData.Invoke(baseIdData, _uniqueID);
            
        }
        public void Load()
        {
            BaseIdData baseIdData = SaveLoadSignals.Instance.onLoadIdleData.Invoke(BaseIdData.BaseKey, _uniqueID);
        }
        #endregion
    }
}