﻿using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType,Transform, GameObject> onGetPoolObject = delegate{ return default; };
        public UnityAction<PoolType,GameObject> onReleasePoolObject = delegate {  };
    }
}