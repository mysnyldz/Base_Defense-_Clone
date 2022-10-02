using UnityEngine;

namespace Command.BaseCommands
{
    public class ClearActiveBaseCommand
    {
        public void ClearActiveBase(Transform baseHolder)
        {
            Object.Destroy(baseHolder.GetChild(0).gameObject);
        }
    }
}