using System;
using Cinemachine;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class TurretMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private TurretManager manager;

        #endregion

        #region Private Variables

        private float _turretRotX;

        #endregion

        #endregion

        public void TurretRotation(InputParams data)
        {
            _turretRotX = data.InputValues.x;
            if (manager.TurretStates == TurretStates.PlayerOnTurret)
            {
                TakeAim();
            }
            
        }

        private void TakeAim()
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, -transform.rotation.y, 0),
                Quaternion.Euler(0, -Mathf.Clamp(_turretRotX * 45, -45, 45), 0), 1f);
        }
    }
}