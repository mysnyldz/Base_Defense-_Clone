using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public Func<int> onGetBaseCount = delegate { return default;};
    }
}