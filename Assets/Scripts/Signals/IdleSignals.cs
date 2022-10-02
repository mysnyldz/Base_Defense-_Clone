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
        public Func<GameObject> onGetMineGemVeinTarget = () => default;
        public Func<GameObject> onGetMineDepotTarget = () => default;
        public Func<CD_MineZoneData> onGetMineZoneData = () => default;
        public UnityAction<GameObject> onDepotAddGem = delegate{  };
        public UnityAction<GameObject> onPlayerEnterDepot = delegate{  };
        #endregion
        
        public Func<GameObject> onGetAmmoArea = () => default;

    }
}