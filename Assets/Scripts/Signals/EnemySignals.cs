using System;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class EnemySignals : MonoSingleton <EnemySignals>
    {
        public Func<GameObject> onGetBasePoints = delegate { return default;};
        public Func<GameObject> onGetPlayerPoints = delegate { return default;};
        public Func<GameObject> onGetMineTntPoints = delegate { return default;};
    }
}