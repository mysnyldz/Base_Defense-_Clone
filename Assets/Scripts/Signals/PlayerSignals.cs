using System;
using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        #region HealthSignals

        public UnityAction<int> onHealthUpdate = delegate { };
        public UnityAction onHealtBarRotationZero = delegate { };

        #endregion


        #region MoneySignals

        public UnityAction<GameObject> onGetMoneyOldParent = delegate { };

        #endregion

        #region EnemySignals

        public UnityAction<GameObject> onEnemyAddTargetList = delegate { };
        public UnityAction<GameObject> onEnemyRemoveTargetList = delegate { };
        public UnityAction<bool> onTargetInSight = delegate { };

        #endregion

        #region MineSignals

        public UnityAction<GameObject> onPlayerOnMineExplosion = delegate { };
        public UnityAction<GameObject> onPlayerOffMineExplosion = delegate { };

        #endregion

        #region TurretSignals

        public UnityAction<GameObject, GameObject> onPlayerOnTurret = delegate { };
        public UnityAction<GameObject> onPlayerOutTurret = delegate { };
        public UnityAction<GameObject> onPlayerReadyForShoot = delegate { };

        public UnityAction<GameObject> onAiTurretArea = delegate { };

        //public Func<GameObject> onPlayerMovement = delegate { return default; };
        public Func<GameObject> onGetPlayerParent = delegate { return default; };

        //public Func<List<GameObject>> onGetDepotAmmoBox = delegate { return default; };
        public UnityAction<int, GameObject> onDecreaseBullet = delegate { };

        #endregion
    }
}