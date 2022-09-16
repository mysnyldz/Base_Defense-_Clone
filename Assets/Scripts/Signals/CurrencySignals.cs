using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CurrencySignals : MonoSingleton<CurrencySignals>
    {
        public UnityAction<float> onAddMoney = delegate {  };
        public UnityAction<float> onAddGem = delegate {  };
        
        public UnityAction<float> onReduceMoney = delegate {  };
        public UnityAction<float> onReduceGem = delegate {  };
        
        public Func<float> onGetMoney;
        public Func<float> onGetGem;
        

    }
}