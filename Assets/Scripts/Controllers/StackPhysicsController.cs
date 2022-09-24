using System;
using System.Threading;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class StackPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AmmoStackManager ammoStackManager;
        [SerializeField] private MoneyStackManager moneyStackManager;

        #endregion

        #region Private Variables

        private int _obtainTime = 1;
        private float _timer;

        #endregion

        #endregion

        private void Awake()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                moneyStackManager.AddStack(IdleSignals.Instance.onGetMoney());
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoWarehouse"))
            {
                _timer += Time.fixedDeltaTime;
                if (_timer >= _obtainTime)
                {
                    _timer = 0;
                    ammoStackManager.AddStack(IdleSignals.Instance.onGetAmmo());
                    
                }

                //if (_obtainTime >= 20)
                //{
                //    
                //    _obtainTime = _obtainTime * 50 / 100;
                //}
                //else
                //{
                //    _obtainTime++;
                //}
            }
        }
    }
}