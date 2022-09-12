using Data.ValueObject.PlayerData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "CD_Objects/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}