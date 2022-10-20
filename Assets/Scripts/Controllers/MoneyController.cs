using System;
using System.Collections.Generic;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MoneyController : MonoBehaviour
    {
        private void OnEnable()
        {
            IdleSignals.Instance.onAddMoneyList?.Invoke(gameObject);
        }
    }
}