using UnityEngine;

namespace Command.BaseCommands
{
    public class ClearActiveBaseCommand : MonoBehaviour
    {
        public void ClearActiveBase(Transform baseHolder)
        {
            Destroy(baseHolder.GetChild(0).gameObject);
        }
    }
}