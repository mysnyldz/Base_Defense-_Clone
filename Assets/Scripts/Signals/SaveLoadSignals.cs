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

        public Func<string, int, BaseData> onLoadData;

        public UnityAction onSaveScoreData = delegate {  };
        
        public Func<BaseDataParams> onGetBaseData = delegate { return default;};
        public Func<BaseDataParams> onLoadBaseData = delegate { return default;};
        
        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default;};
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default;};


    }
}