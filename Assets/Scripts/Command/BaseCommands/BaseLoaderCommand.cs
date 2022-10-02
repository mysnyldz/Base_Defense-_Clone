using UnityEngine;

namespace Command.BaseCommands
{
    public class BaseLoaderCommand
    {
        public void InitializeLevel(int _baseID, Transform baseHolder)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/Bases/Base {_baseID}"), baseHolder);
        }
    }
}