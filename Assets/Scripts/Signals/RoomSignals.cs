using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class RoomSignals : MonoSingleton<RoomSignals>
    {
        public UnityAction onRoomComplete = delegate {  };
        
    }
}