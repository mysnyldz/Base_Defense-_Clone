using System;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class EnemySignals : MonoSingleton <EnemySignals>
    {
        public Func<GameObject> onGetBasePoints = () => default;
        public Func<GameObject> onGetPlayerPoints = () => default;
        public Func<GameObject> onGetMineTntPoints = () => default;
    }
}