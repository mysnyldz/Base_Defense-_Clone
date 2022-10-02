using System;
using Data.ValueObject;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class RoomSignals : MonoSingleton<RoomSignals>
    {
        public UnityAction<GameObject> onBuyRoomArea = delegate {  };
        public UnityAction onRoomComplete = delegate {  };
        public Func<RoomTypes,RoomIDData> onRoomData = delegate{return  default;};
        public UnityAction onInitializeRoom = delegate {  } ;
    }
    
}