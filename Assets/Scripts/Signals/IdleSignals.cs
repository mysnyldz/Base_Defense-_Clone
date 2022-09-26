using System;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleSignals : MonoSingleton<IdleSignals>
    {
        #region Mine Signals
        public Func<GameObject> onGetMineGemVeinTarget = delegate { return default;};
        public Func<GameObject> onGetMineDepotTarget = delegate { return default;};
        public Func<CD_MineZoneData> onGetMineZoneData = delegate { return default;};
        public UnityAction<GameObject> onDepotAddGem = delegate{  };
        #endregion

    }
}