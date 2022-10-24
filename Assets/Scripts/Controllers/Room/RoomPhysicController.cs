using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Room
{
    public class RoomPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;

        #endregion

        #region Private Variables

        private float _timer = 0;
        private float _spendTime = 1;
        

        #endregion

        #endregion
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timer += (Time.fixedDeltaTime)*100;
                if (_timer >= _spendTime)
                {
                    roomManager.OnBuyRoomArea();
                    //RoomSignals.Instance.onBuyRoomArea?.Invoke(roomManager.gameObject);
                    _timer = 0;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               // roomManager.StopTimer();
            }
        }
    }
}