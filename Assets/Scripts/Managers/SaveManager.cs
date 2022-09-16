using Command.SaveLoadCommands;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadCommand _loadCommand;
        private SaveCommand _saveCommand;
 

        #endregion
        
        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveLoadSignals.Instance.onSaveIdleData += OnSaveIdleData;
            SaveLoadSignals.Instance.onSaveScoreData += OnSaveScoreData;
            SaveLoadSignals.Instance.onLoadBaseData += OnLoadIdleData;
            SaveLoadSignals.Instance.onLoadScoreData += OnLoadScoreData;
        }


        private void Unsubscribe()
        {
            SaveLoadSignals.Instance.onSaveIdleData -= OnSaveIdleData;
            SaveLoadSignals.Instance.onSaveScoreData -= OnSaveScoreData;
            SaveLoadSignals.Instance.onLoadBaseData -= OnLoadIdleData;
            SaveLoadSignals.Instance.onLoadScoreData -= OnLoadScoreData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void Awake()
        {
            Initialization();
         
        }

        private void Initialization()
        {
            _loadCommand = new LoadCommand();
            _saveCommand = new SaveCommand(); 
        }

        private void OnSaveIdleData()
        {
            _saveCommand.Execute();
        }

        private void OnSaveScoreData()
        {
        }
        
        private void SaveIdleData(BaseDataParams baseDataParams)
        {
            
        }

        private void OnLoadIdleData()
        {
        }


        private void OnLoadScoreData()
        {
        }
    }
}