using UnityEngine;

namespace Command.BaseCommands
{
    public class BaseLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int _baseID, Transform baseHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/Bases/Base {_baseID}"), baseHolder);
        }
    }
}