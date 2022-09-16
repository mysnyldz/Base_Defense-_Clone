using Command.SaveLoadCommands;
using Data.ValueObject;
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

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            _loadCommand = new LoadCommand();
            _saveCommand = new SaveCommand();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveLoadSignals.Instance.onSaveIdleData += _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleData += _loadCommand.Execute<BaseIdData>;
            
            SaveLoadSignals.Instance.onSaveCurrencyData += _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadCurrencyData += _loadCommand.Execute<CurrencyIdData>;
        }


        private void Unsubscribe()
        {
            SaveLoadSignals.Instance.onSaveIdleData -= _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleData -= _loadCommand.Execute<BaseIdData>;
            
            SaveLoadSignals.Instance.onSaveCurrencyData -= _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadCurrencyData -= _loadCommand.Execute<CurrencyIdData>;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion
    }
}