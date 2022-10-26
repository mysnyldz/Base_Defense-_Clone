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
        public bool TurretMode, BattleMode, IdleMode, TargetMode, DeathMode;
        public PlayerStateTypes playerTypes;
        public GameObject Target;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerHealthController healthController;
        [SerializeField] private AmmoStackController ammoStackController;
        [SerializeField] private MoneyStackController moneyStackController;
        [SerializeField] private PlayerTargetManager playerTargetManager;
        [SerializeField] private GameObject playerHolder;
        [SerializeField] public GameObject playerSphere;

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
            CoreGameSignals.Instance.onFailed += OnFailed;
            CoreGameSignals.Instance.onTryAgain += OnTryAgain;
            InputSignals.Instance.onInputDragged += OnPlayerInput;
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            IdleSignals.Instance.onGetAmmoStackController += OnGetAmmoStackController;
            // PlayerSignals.Instance.onPlayerMovement += OnPlayerMovement;
            PlayerSignals.Instance.onGetPlayerParent += OnGetPlayerParent;
            PlayerSignals.Instance.onGetMoneyOldParent += OnGetMoneyOldParent;
            PlayerSignals.Instance.onEnemyAddTargetList += OnEnemyAddTargetList;
            PlayerSignals.Instance.onEnemyRemoveTargetList += OnEnemyRemoveTargetList;
            PlayerSignals.Instance.onHealthUpdate += OnHealthUpdate;
            PlayerSignals.Instance.onHealtBarRotationZero += OnHealtBarRotationZero;
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            CoreGameSignals.Instance.onTryAgain -= OnTryAgain;
            InputSignals.Instance.onInputDragged -= OnPlayerInput;
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            IdleSignals.Instance.onGetAmmoStackController -= OnGetAmmoStackController;
            // PlayerSignals.Instance.onPlayerMovement -= OnPlayerMovement;
            PlayerSignals.Instance.onGetPlayerParent -= OnGetPlayerParent;
            PlayerSignals.Instance.onGetMoneyOldParent -= OnGetMoneyOldParent;
            PlayerSignals.Instance.onEnemyAddTargetList -= OnEnemyAddTargetList;
            PlayerSignals.Instance.onEnemyRemoveTargetList -= OnEnemyRemoveTargetList;
            PlayerSignals.Instance.onHealthUpdate -= OnHealthUpdate;
            PlayerSignals.Instance.onHealtBarRotationZero -= OnHealtBarRotationZero;
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

        public void ChangeState(PlayerStateTypes types)
        {
            playerTypes = types;
            animationController.SetPlayerAnimationStateTypes(types);
            playerTargetManager.SetPlayerTargetStateTypes(types);
            healthController.HealthBarVisibles(types);
            if (types == PlayerStateTypes.Death)
            {
                moneyStackController.DeathDecreaseStack();
            }
        }


        private void OnPlayerInput(InputParams inputParams)
        {
            switch (playerTypes)
            {
                case PlayerStateTypes.Turret:
                    movementController.UpdateInputValue(inputParams);
                    break;
                case PlayerStateTypes.Death:
                    movementController.DisableMovement();
                    break;
                default:
                    movementController.UpdateInputValue(inputParams);
                    animationController.ChangeVelocity(inputParams);
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

        private void OnHealthUpdate(int health)
        {
            healthController.DecreaseHealth(health);
        }

        private void OnHealtBarRotationZero()
        {
            healthController.HealtBarRotation();
        }

        public void AmmoAddStack()
        {
            ammoStackController.OnGetAmmo();
        }

        public void AmmoSendAmmoWareHouse()
        {
            ammoStackController.SendAmmoWareHouse();
        }

        public void AmmoDecreaseStack()
        {
            ammoStackController.DecreaseStack();
        }

        private void OnEnemyAddTargetList(GameObject obj)
        {
            playerTargetManager.OnAddTargetList(obj);
        }

        private void OnEnemyRemoveTargetList(GameObject obj)
        {
            playerTargetManager.OnRemoveTargetList(obj);
        }

        private void OnGetMoneyOldParent(GameObject parent)
        {
            moneyStackController.OldParent = parent;
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            ChangeState(PlayerStateTypes.Idle);
        }

        private void OnTryAgain()
        {
            gameObject.transform.localPosition = Vector3.zero;
            movementController.IsReadyToPlay(true);
            ChangeState(PlayerStateTypes.Idle);
        }

        private void OnFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnReset()
        {
            gameObject.SetActive(false);
        }

        private AmmoStackController OnGetAmmoStackController() => ammoStackController;
        private GameObject OnGetPlayerParent() => playerHolder;
    }
}