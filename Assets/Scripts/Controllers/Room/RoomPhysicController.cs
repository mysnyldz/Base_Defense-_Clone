using Managers;
using UnityEngine;

namespace Controllers.Room
{
    public class RoomPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;

        #endregion

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                roomManager.StartTimer();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                roomManager.StopTimer();
            }
        }
    }
}