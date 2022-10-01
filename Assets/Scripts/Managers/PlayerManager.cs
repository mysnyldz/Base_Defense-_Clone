using System;
using Controllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.PlayerData;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;
        [Header("Data")] public AmmoStackData AmmoStackData;
        [Header("Data")] public MoneyStackData MoneyStackData;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private PlayerPhysicsController physicsController;

        [SerializeField] private AmmoStackController ammoStackController;

        [SerializeField] private MoneyStackController moneyStackController;

        #endregion

        #region Private Variables

        [SerializeField] private Rigidbody playerRigidbody;

        [SerializeField] private CapsuleCollider playerCollider;

        #endregion

        #endregion

        private void Awake()
        {
            Data = GetPlayerData();
            MoneyStackData = GetMoneyStackData();
            AmmoStackData = GetAmmoStackData();
            Init();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private MoneyStackData GetMoneyStackData() => Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;
        private AmmoStackData GetAmmoStackData() => Resources.Load<CD_AmmoStackData>("Data/CD_AmmoStackData").Data;

        private void Init()
        {
            movementController = GetComponent<PlayerMovementController>();
            SetDataToControllers();
            
        }

        private void SetDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnGetInputValues(InputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        public void ChangePlayerAnimation(PlayerAnimTypes animType)
        {
            animationController.ChangeAnimationState(animType);
        }

        public void MoneyAddStack(GameObject obj)
        {
            moneyStackController.AddStack(obj);
        } 
        public GameObject MoneyDecreaseStack()
        {
           return moneyStackController.DecreaseStack();
        } 
        
        public void AmmoAddStack()
        {
            ammoStackController.OnGetAmmo();
        }
        public GameObject AmmoDecreaseStack()
        {
            return ammoStackController.DecreaseStack();
        }
        

        private void OnPlay() => movementController.IsReadyToPlay(true);

        private void OnReset()
        {
            gameObject.SetActive(false);
        }
    }
}