using UnityEngine;
using System;
using UnityEngine.Events;
using Extentions;


namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {

        public UnityAction onBaseInitialize = delegate { };

        public UnityAction onClearActiveBase = delegate { };

        public UnityAction onFailed = delegate { };

        public UnityAction onNextBase = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onSetCameraTarget = delegate { };
        
        public UnityAction onApplicationPause = delegate { };
        
        public UnityAction onApplicationQuit = delegate { };

        public UnityAction onEnterTurret = delegate {  };
        public UnityAction onExitTurret = delegate {  };
        public UnityAction onEnterDrone = delegate {  };
    }
}
