using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager _manager;

        #endregion

        #region Private Variables

        private int _obtainTime = 1;
        private float _timer;
        private int _maxAmmoCount = 0;
        private int _maxMoneyCount = 0;
        

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                if (_maxMoneyCount < _manager.MoneyStackData.MaxMoneyCount)
                {
                    _manager.MoneyAddStack(other.gameObject);
                    _maxMoneyCount++;
                    other.GetComponent<Rigidbody>().useGravity = false;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    other.GetComponent<Collider>().enabled = false;
                    
                }
                
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoWarehouse"))
            {
                _timer += Time.fixedDeltaTime;
                if (_timer >= _obtainTime && _maxAmmoCount <= _manager.AmmoStackData.MaxAmmoCount)
                {
                    _timer = 0;
                    _manager.AmmoAddStack();
                    _maxAmmoCount++;
                }
            }
        }
    }
}