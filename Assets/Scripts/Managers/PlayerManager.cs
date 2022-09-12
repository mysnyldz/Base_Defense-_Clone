using Controllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
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

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private PlayerPhysicsController physicsController;

        #endregion

        #region Private Variables

        [SerializeField] private Rigidbody playerRigidbody;

        [SerializeField] private CapsuleCollider playerCollider;

        #endregion

        #endregion

        private void Awake()
        {
            Data = GetPlayerData();
            Init();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

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

        public void GateControlller()
        {
            
        }
        

        private void OnPlay() => movementController.IsReadyToPlay(true);

        private void OnReset()
        {
            gameObject.SetActive(false);
        }
    }
}