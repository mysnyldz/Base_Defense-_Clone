﻿using Managers;
using UnityEngine.AI;

namespace States.SupporterMoneyStates
{
    public class MoneyBaseState
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        #endregion

        #region Private Variables

        private SupporterMoneyManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public MoneyBaseState(ref SupporterMoneyManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }
    }
}