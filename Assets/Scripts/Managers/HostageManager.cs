using System;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Enums;
using Signals;
using States.HostageStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class HostageManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject Player;
        public GameObject Tent;
        public HostageStatesTypes HostageStateTypes;

        #endregion

        #region Serializefield Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private HostageAnimationController animationController;

        #endregion

        #region Private Variables

        private HostageBaseState _currentHostageBaseState;
        private HostageFollowState _hostageFollowState;
        private HostageIdleState _hostageIdleState;
        private HostageMoveTent _hostageMoveTent;
        private HostageAnimTypes _hostageAnimTypes;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            GetReferences();
        }


        private void OnEnable()
        {
            _currentHostageBaseState = _hostageIdleState;
            _currentHostageBaseState.EnterState();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }


        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetReferences()
        {
            var manager = this;
            _hostageFollowState = new HostageFollowState(ref manager, ref agent);
            _hostageIdleState = new HostageIdleState(ref manager, ref agent);
            _hostageMoveTent = new HostageMoveTent(ref manager, ref agent);

        }


        private void Update()
        {
            _currentHostageBaseState.UpdateState();
        }
        
        private void ChangeState(HostageStatesTypes types)
        {
            HostageStateTypes = types;
            animationController.SetPlayerAnimationStateTypes(types);
        }

        public void SwitchState(HostageStatesTypes state)
        {
            switch (state)
            {
                case HostageStatesTypes.Idle:
                    _currentHostageBaseState = _hostageIdleState;
                    ChangeState(state);
                    break;
                case HostageStatesTypes.Follow:
                    _currentHostageBaseState = _hostageFollowState;
                    ChangeState(state);
                    break;
                case HostageStatesTypes.Tent:
                    _currentHostageBaseState = _hostageMoveTent;
                    ChangeState(state);
                    break;
            }

            _currentHostageBaseState.EnterState();
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentHostageBaseState.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _currentHostageBaseState.OnTriggerEnter(other);
        }
    }
}