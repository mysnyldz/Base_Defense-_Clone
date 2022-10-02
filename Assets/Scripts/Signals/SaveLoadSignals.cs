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
        public UnityAction<BaseIdData,int> onSaveBaseData = delegate {  };

        public Func<string,int, BaseIdData> onLoadBaseData;
        
        public UnityAction<RoomIDData,int> onSaveRoomData = delegate {  };

        public Func<string,int, RoomIDData> onLoadRoomData;

        public UnityAction<CurrencyIdData,int> onSaveCurrencyData = delegate {  };
        
        public Func<string,int, CurrencyIdData> onLoadCurrencyData;
        
    }
}