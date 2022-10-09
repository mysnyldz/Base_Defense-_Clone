using Data.ValueObject.PlayerData;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,

        [SerializeField] private PlayerManager playerManager;

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private bool isReadyToMove, isReadyToPlay, isOnTurret;

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

        public void DeactiveMovement()
        {
            isReadyToMove = false;
        }

        public void IsReadyToPlay(bool state) => isReadyToPlay = state;

        public void IsOnTurret(bool state) => isOnTurret = state;

        private void FixedUpdate()
        {
            if (isReadyToPlay)
            {
                if (isReadyToMove && isOnTurret != true )
                {
                    Move();
                }
                else if (isOnTurret)
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

        private void Turret()
        {
            if (_movementDirection.z * _movementData.PlayerJoystickSpeed <= -2f)
            {
                isOnTurret = false;
                PlayerSignals.Instance.onPlayerOutTurret?.Invoke(playerManager.gameObject);
            }
            
            playerManager.ChangePlayerAnimation(PlayerAnimTypes.Turret);
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

            playerManager.ChangePlayerAnimation(PlayerAnimTypes.Run);

            if (_movementDirection != Vector3.zero)
            {
                var _newDirect = Quaternion.LookRotation(_movementDirection);
                rigidbody.transform.GetChild(0)
                    .rotation = _newDirect;
            }
        }

        private void Stop()
        {
            playerManager.ChangePlayerAnimation(PlayerAnimTypes.Idle);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
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