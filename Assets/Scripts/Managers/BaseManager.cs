using System.Threading.Tasks;
using Command.BaseCommands;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BaseData")] public BaseData BaseData;

        [Header("BaseIdData")] public BaseIdData BaseIdData;

        #endregion Public Variables

        #region Serialized Variables

        [Space] [SerializeField] private GameObject BaseHolder;

        [SerializeField] private BaseLoaderCommand baseLoader;
        [SerializeField] private ClearActiveBaseCommand baseClearer;

        #endregion Serialized Variables

        #region Private Variables

        private CD_Base _cdBase;

        private int _baseID;

        // private int _gameScore;
        private int _uniqueID = 1234;

        #endregion Private Variables

        #endregion Self Variables

        #region MonoBehavior Methods

        private void Awake()
        {
            GetData();
        }

        private void Start()
        {
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
                //    Save(_uniqueID);
                }
            }

            //Load(_uniqueID);
            
            BaseData = GetBaseData();
        }

        private BaseData GetBaseData()
        {
            var newBaseData = _baseID % Resources.Load<CD_Base>("Data/CD_Base").Bases.Count;
            return Resources.Load<CD_Base>("Data/CD_Base").Bases[newBaseData];
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
          //  Save(_uniqueID);
        }


        #region Level Management

        private void OnNextLevel()
        {
            _baseID++;
           // Save(_uniqueID);
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
            int newBaseData = _baseID % Resources.Load<CD_Base>($"Data/CD_Base").Bases.Count;
            baseLoader.InitializeLevel(newBaseData, BaseHolder.transform);
        }

        private void OnClearActiveBase()
        {
            baseClearer.ClearActiveBase(BaseHolder.transform);
        }

        #endregion

       // #region Level Save and Load
//
       // public void Save(int uniqueId)
       // {
       //     BaseIdData baseIdData = new BaseIdData(_baseID);
//
       //     //SaveLoadSignals.Instance.onSaveGameData.Invoke(baseIdData, uniqueId);
       // }
//
       // public void Load(int uniqueId)
       // {
       //     // BaseIdData levelIdData = SaveLoadSignals.Instance.onLoadGameData.Invoke(BaseIdData.BaseKey, uniqueId);
//
       //     _baseID = BaseIdData.BaseId;
       // }
//
       // #endregion
    }
}