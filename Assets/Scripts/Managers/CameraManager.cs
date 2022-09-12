using Cinemachine;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeField

        [SerializeField] private CameraMovementController cameraMovementController;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onEnterTurret += OnEnterTurret;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterDrone += OnEnterDrone;
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterTurret -= OnEnterTurret;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterDrone -= OnEnterDrone;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnPlay()
        {
            cameraMovementController.GameStart();
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }

        private void OnEnterTurret()
        {
            cameraMovementController.EnterTurret();
        }

        private void OnEnterDrone()
        {
            cameraMovementController.EnterTurret();
        }

        private void OnSetCameraTarget()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            stateDrivenCamera.Follow = playerManager.transform;
        }

        private void OnReset()
        {
            cameraMovementController.CamReset();
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }
    }
}