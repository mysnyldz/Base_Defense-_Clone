using System;
using Data.ValueObject.PlayerData;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private PlayerTargetController targetController;


        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private bool isReadyToMove, isReadyToPlay, TurretMode, BattleMode, IdleMode, TargetMode;

        #endregion

        #region Private Variables

        private PlayerMovementData _movementData;

        private Vector3 _movementDirection;

        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData movementData)
        {
            _movementData = movementData;
        }

        public void UpdateInputValue(InputParams inputParam)
        {
            _movementDirection = inputParam.InputValues;
        }

        public void EnableMovement()
        {
            isReadyToMove = true;
        }

        public void DisableMovement()
        {
            isReadyToMove = false;
        }

        public void IsReadyToPlay(bool state) => isReadyToPlay = state;

        public void IsOnTurret() => TurretMode = PlayerSignals.Instance.onGetIsTurretMode.Invoke();
        public void IsOnIdle() => IdleMode = PlayerSignals.Instance.onGetIsIdleMode.Invoke();
        public void IsOnBattle() => BattleMode = PlayerSignals.Instance.onGetIsBattleMode.Invoke();


        private void FixedUpdate()
        {
            if (isReadyToPlay)
            {
                if (isReadyToMove)
                {
                    Move();
                }
                else if (playerManager.playerTypes == PlayerStateTypes.Turret)
                {
                    Turret();
                }
                else
                {
                    Stop();
                }
            }
            else
            {
                Stop();
            }
        }

        // private void Update()
        // {
        //     BattleMode = playerManager.BattleMode;
        //     IdleMode = playerManager.IdleMode;
        //     TurretMode = playerManager.TurretMode;
        //     TargetMode = playerManager.TargetMode;
        // }

        private void Turret()
        {
            Debug.Log(_movementDirection.z);
            if (_movementDirection.z <= -0.3f)
            {
                PlayerSignals.Instance.onPlayerOutTurret?.Invoke(playerManager.gameObject);
                playerManager.playerTypes = PlayerStateTypes.Idle;
            }
        }

        private void Move()
        {
            var movement = rigidbody.velocity;
            movement = new Vector3(_movementDirection.x * _movementData.PlayerJoystickSpeed,
                0,
                _movementDirection.z * _movementData.PlayerJoystickSpeed);
            rigidbody.velocity = movement;

            Vector3 position;
            position = new Vector3(rigidbody.position.x, (position = rigidbody.position).y, position.z);
            rigidbody.position = position;


            if (_movementDirection != Vector3.zero)
            {
                var _newDirect = Quaternion.LookRotation(_movementDirection);
                rigidbody.transform.GetChild(0)
                    .rotation = _newDirect;
            }
        }


        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            isReadyToMove = false;
        }

        public void MoveReset()
        {
            Stop();

            isReadyToPlay = false;

            isReadyToMove = false;

            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}