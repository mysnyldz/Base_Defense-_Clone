using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CurrencySignals : MonoSingleton<CurrencySignals>
    {
        public UnityAction<int> onAddMoney = delegate {  };
        public UnityAction<int> onAddGem = delegate {  };
        
        public UnityAction<int> onReduceMoney = delegate {  };
        public UnityAction<int> onReduceGem = delegate {  };
        
        public Func<int> onGetMoney;
        public Func<int> onGetGem;
        

    }
}