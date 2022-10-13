using System;
using System.Collections.Generic;
using Controllers;
using Controllers.Player;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.PlayerData;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
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
        [Header("Data")] public TurretData TurretData;
        public bool TurretMode, BattleMode, IdleMode, TargetMode;
        public PlayerStateTypes playerTypes;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private PlayerPhysicsController physicsController;

        [SerializeField] private AmmoStackController ammoStackController;

        [SerializeField] private MoneyStackController moneyStackController;

        [SerializeField] private PlayerTargetController playerTargetController;

        [SerializeField] private GameObject playerHolder;
        
        [SerializeField] private List<GameObject> targets;

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
            TurretData = GetTurretData();
            playerTypes = PlayerStateTypes.Idle;
            Init();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private MoneyStackData GetMoneyStackData() => Resources.Load<CD_MoneyStackData>("Data/CD_MoneyStackData").Data;
        private AmmoStackData GetAmmoStackData() => Resources.Load<CD_AmmoStackData>("Data/CD_AmmoStackData").Data;
        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;

        private void Init()
        {
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
            InputSignals.Instance.onInputDragged += OnPlayerInput;
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            IdleSignals.Instance.onGetAmmoStackController += OnGetAmmoStackController;
            PlayerSignals.Instance.onPlayerMovement += OnPlayerMovement;
            PlayerSignals.Instance.onPlayerOnTurretAnimation += OnPlayerOnTurretAnimation;
            PlayerSignals.Instance.onGetPlayerParent += OnGetPlayerParent;
            PlayerSignals.Instance.onGetIsBattleMode += OnGetIsBattleMode;
            PlayerSignals.Instance.onGetIsIdleMode += OnGetIsIdleMode;
            PlayerSignals.Instance.onGetIsTurretMode += OnGetIsTurretMode;
            PlayerSignals.Instance.onEnemyAddTargetList += OnEnemyAddTargetList;
            PlayerSignals.Instance.onEnemyRemoveTargetList += OnEnemyRemoveTargetList;
        }



        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputDragged -= OnPlayerInput;
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            IdleSignals.Instance.onGetAmmoStackController -= OnGetAmmoStackController;
            PlayerSignals.Instance.onPlayerMovement -= OnPlayerMovement;
            PlayerSignals.Instance.onPlayerOnTurretAnimation -= OnPlayerOnTurretAnimation;
            PlayerSignals.Instance.onGetPlayerParent -= OnGetPlayerParent;
            PlayerSignals.Instance.onGetIsBattleMode -= OnGetIsBattleMode;
            PlayerSignals.Instance.onGetIsIdleMode -= OnGetIsIdleMode;
            PlayerSignals.Instance.onGetIsTurretMode -= OnGetIsTurretMode;
            PlayerSignals.Instance.onEnemyAddTargetList -= OnEnemyAddTargetList;
            PlayerSignals.Instance.onEnemyRemoveTargetList -= OnEnemyRemoveTargetList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DisableMovement();
            var inputparams = new InputParams()
            {
                InputValues = Vector3.zero
            };
            animationController.ChangeVelocity(inputparams);
        }

        public void SetPlayerStateTypes(PlayerStateTypes types)
        {
            playerTypes = types;
            switch (playerTypes)
            {
                case PlayerStateTypes.Idle:
                    animationController.ChangeAnimationState(PlayerAnimTypes.IdleMode);
                    break;
                case PlayerStateTypes.Battle:
                    animationController.ChangeAnimationState(PlayerAnimTypes.BattleMode);
                    break;
                case PlayerStateTypes.Target:
                    animationController.ChangeAnimationState(PlayerAnimTypes.TargetMode);
                    break;
                case PlayerStateTypes.Turret:
                    animationController.ChangeAnimationState(PlayerAnimTypes.TurretMode);
                    break;
            }
        }


        private void OnPlayerInput(InputParams inputParams)
        {
            switch (playerTypes)
            {
                case PlayerStateTypes.Idle:
                    movementController.UpdateInputValue(inputParams);
                    animationController.ChangeVelocity(inputParams);
                    break;
                case PlayerStateTypes.Battle:
                    movementController.UpdateInputValue(inputParams);
                    animationController.ChangeVelocity(inputParams);
                    break;
                case PlayerStateTypes.Target:
                    movementController.UpdateInputValue(inputParams);
                    animationController.ChangeVelocity(inputParams);
                    break;
                case PlayerStateTypes.Turret:
                    movementController.UpdateInputValue(inputParams);
                    break;
            }
        }

        public void MoneyAddStack(GameObject obj)
        {
            moneyStackController.AddStack(obj);
        }

        public void MoneyDecreaseStack()
        {
            moneyStackController.DecreaseStack();
        }

        public void AmmoAddStack()
        {
            ammoStackController.OnGetAmmo();
        }

        public void AmmoDecreaseStack()
        {
            ammoStackController.DecreaseStack();
        }
        private void OnEnemyAddTargetList(GameObject obj)
        {
            playerTargetController.OnAddTargetList(obj);
        }
        private void OnEnemyRemoveTargetList(GameObject obj)
        {
            playerTargetController.OnRemoveTargetList(obj);
        }
        

        private GameObject OnPlayerMovement()
        {
            return movementController.gameObject;
        }

        private bool OnGetIsTurretMode() => TurretMode;


        private bool OnGetIsIdleMode() => IdleMode;

        private bool OnGetIsBattleMode() => BattleMode;

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            animationController.ChangeAnimationState(PlayerAnimTypes.IdleMode);
        }

        public void IsTurretState()
        {
            playerTypes = PlayerStateTypes.Turret;
        }

        private void OnPlayerOnTurretAnimation()
        {
            movementController.IsOnTurret();
        }

        private void OnReset()
        {
            gameObject.SetActive(false);
        }

        private AmmoStackController OnGetAmmoStackController() => ammoStackController;
        private GameObject OnGetPlayerParent() => playerHolder;
    }
}