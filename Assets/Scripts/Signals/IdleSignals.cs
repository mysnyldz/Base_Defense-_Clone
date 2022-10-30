using System;
using System.Collections.Generic;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleSignals : MonoSingleton<IdleSignals>
    {
        #region Mine Signals

        public Func<GameObject> onGetMineGemVeinTarget = delegate { return default; };
        public Func<GameObject> onGetMineDepotTarget = delegate { return default; };
        public Func<CD_MineZoneData> onGetMineZoneData = delegate { return default; };
        public UnityAction<GameObject> onDepotAddGem = delegate { };
        public UnityAction<GameObject> onPlayerEnterGemDepot = delegate { };
        public UnityAction onMineZoneAddMiner = delegate { };

        #endregion


        #region Ammo Signals

        public Func<GameObject> onGetAmmoArea = delegate { return default; };
        public Func<GameObject> onGetAmmoDepotTarget = delegate { return default; };
        public UnityAction<GameObject> onDepotAddAmmo = delegate { };
        public UnityAction<GameObject> onPlayerEnterTurretDepot = delegate { };
        public Func<AmmoStackController> onGetAmmoStackController = delegate { return default; };

        public Func<int> onGetDamage = delegate { return default; };

        #endregion

        #region Money Supporter Signals

        public UnityAction<GameObject> onAddMoneyList = delegate { };
        public UnityAction<GameObject> onRemoveMoneyList = delegate { };
        public Func<GameObject> onMoneySupporterBasePoints = delegate { return default; };
        public UnityAction<int> onMoneySupporterBuyArea = delegate { };
        public Func<SupporterAreaManager> onGetSupporterAreaManager = delegate { return default;};
        public Func<GameObject> onMoneyGetTakenPoints = delegate { return default; };
        

        #endregion


        #region Hostage Signals

        public UnityAction<GameObject> onRemoveHostageSpawnPoint=delegate {  };

        #endregion
    }
}