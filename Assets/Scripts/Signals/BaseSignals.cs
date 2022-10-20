using System;
using System.Net.Sockets;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public Func<int> onGetBaseCount = delegate { return default;};
    }
}