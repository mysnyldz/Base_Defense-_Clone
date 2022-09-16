using System;
using Data.ValueObject;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<BaseIdData,int> onSaveIdleData = delegate {  };

        public Func<string, int, BaseIdData> onLoadIdleData;

        public UnityAction<CurrencyIdData,int> onSaveCurrencyData = delegate {  };
        
        public Func<string,int, CurrencyIdData> onLoadCurrencyData;
        
    }
}