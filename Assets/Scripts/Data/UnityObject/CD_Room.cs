using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Room", menuName = "CD_Objects/CD_Room", order = 0)]
    public class CD_Room : ScriptableObject
    {
        public RoomData Data;
    }
}