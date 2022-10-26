using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton <EnemySignals>
    {
        public Func<GameObject> onGetBasePoints = delegate { return default;};
        public Func<GameObject> onGetPlayerPoints = delegate { return default;};
        public Func<GameObject> onGetMineTntPoints = delegate { return default;};
        public UnityAction<bool> onEnemyDeathStatus= delegate {};
        public UnityAction<int,GameObject> onTakeDamage = delegate {  };
        
    }
}