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

        #region AmmoSignals

        public Func<GameObject> onGetAmmo = delegate { return default;};
        public Func<GameObject> onGetAmmoController = delegate { return default;};
        public Func<GameObject> onGetAmmoManager = delegate { return default;};
        


        #endregion

        #region MoneySignals

        public Func<GameObject> onGetMoney = delegate { return default;};
        public Func<GameObject> onGetMoneyController = delegate { return default;};
        public Func<GameObject> onGetMoneyManager = delegate { return default;};

        #endregion
        
    }
}