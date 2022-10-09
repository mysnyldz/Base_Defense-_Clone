using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        #region TurretSignals
        
        public UnityAction<GameObject> onPlayerOnTurret = delegate {  };
        public UnityAction<GameObject> onPlayerOutTurret = delegate {  };
        public UnityAction<PlayerAnimTypes> onPlayerOnTurretAnimation = delegate {  };
        public Func<GameObject> onPlayerMovement = delegate { return default;};
        public Func<GameObject> onGetPlayerParent = delegate { return default;};

        #endregion
    }
}