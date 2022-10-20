using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class GateController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject gate;

        #endregion

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneySupporter"))
            {
                GateOpen();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneySupporter"))
            {
                GateClose();
            }
        }

        public void GateOpen()
        {
            gate.transform.DOLocalRotate(new Vector3(0, 0, -90f), 1.5f);
        }
        public void GateClose()
        {
            gate.transform.DOLocalRotate(Vector3.zero, 1.5f);
        }
    }
}