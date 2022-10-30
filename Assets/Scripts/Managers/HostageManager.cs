using System;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
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
        public HostageController HostageController;
        public int CurrentMinerAmount;
        public int MaxMinerCount;
        public NavMeshAgent agent;

        #endregion

        #region Serializefield Variables

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
            Getdata();
            GetReferences();
        }

        private void Getdata()
        {
           CurrentMinerAmount = Resources.Load<CD_MineZoneData>("Data/CD_MineZoneData").Data.CurrentMinerAmount;
           MaxMinerCount = Resources.Load<CD_MineZoneData>("Data/CD_MineZoneData").Data.MaxMinerCapacity;
        }


        private void OnEnable()
        {
            _currentHostageBaseState.EnterState();
        }


        private void OnDisable()
        {
            _currentHostageBaseState = _hostageIdleState;
        }

        #endregion

        private void GetReferences()
        {
            var manager = this;
            _hostageFollowState = new HostageFollowState(ref manager, ref agent);
            _hostageIdleState = new HostageIdleState(ref manager, ref agent);
            _hostageMoveTent = new HostageMoveTent(ref manager, ref agent);
            _currentHostageBaseState = _hostageIdleState;
        }

        private void Update()
        {
            _currentHostageBaseState.UpdateState();
        }

        public void SetBoolAnimation(HostageAnimTypes types, bool isfollow)
        {
            animationController.SetBoolAnimation(types, isfollow);
        }

        public void SetTriggerAnimation(HostageAnimTypes types)
        {
            animationController.SetTriggerAnimation(types);
        }


        public void SwitchState(HostageStatesTypes state)
        {
            switch (state)
            {
                case HostageStatesTypes.Idle:
                    _currentHostageBaseState = _hostageIdleState;
                    break;
                case HostageStatesTypes.Follow:
                    _currentHostageBaseState = _hostageFollowState;
                    break;
                case HostageStatesTypes.Tent:
                    _currentHostageBaseState = _hostageMoveTent;
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