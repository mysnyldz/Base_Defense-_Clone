using System;

namespace Data.ValueObject.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public int PlayerHealth;
        public PlayerMovementData MovementData;
        
    }
    [Serializable]
    public class PlayerMovementData
    {
        public float PlayerJoystickSpeed = 3;
    }
}