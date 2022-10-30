using Managers;
using UnityEngine;

namespace Controllers
{
    public class AreaPhsicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AreaManager manager;

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
                    manager.OnBuyRoomArea();
                    _timer = 0;
                }
            }
        }
    }
}