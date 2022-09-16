using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction onAddMoney = delegate {  };
        public UnityAction onAddGem = delegate {  };
        public UnityAction onReduceMoney = delegate {  };
        public UnityAction onReduceGem = delegate {  };
        public UnityAction onBuyArea = delegate {  };
    }
}