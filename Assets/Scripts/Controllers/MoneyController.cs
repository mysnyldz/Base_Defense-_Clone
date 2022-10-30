using System;
using System.Collections.Generic;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MoneyController : MonoBehaviour
    {
        [SerializeField] private GameObject oldParent;
        
        private void OnEnable()
        {
            oldParent = gameObject.transform.parent.gameObject;
            
            IdleSignals.Instance.onAddMoneyList?.Invoke(gameObject);
        }
        
        
    }
}