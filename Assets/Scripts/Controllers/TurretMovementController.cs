using System;
using Cinemachine;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using Signals;
using Sirenix.OdinInspector;
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
        [SerializeField] private TurretShootController turretShootController;

        #endregion

        #region Private Variables

        private float _turretRotX;
        [ShowInInspector]private GameObject _target;
        private float _timer;

        #endregion

        #endregion

        public void PlayerTurretRotation(InputParams data)
        {
            _turretRotX = data.InputValues.x;
            if (manager.TurretStates == TurretStates.PlayerOnTurret)
            {
                TakeAim();
            }
        }

        public void AITurretRotation()
        {
            if (manager.TurretStates == TurretStates.AiOnTurret)
            {
                if (turretShootController.Targets.Count >= 1)
                {
                    _target = turretShootController.Targets[0];
                    if (_target != null)
                    {
                        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation,
                            Quaternion.LookRotation(_target.transform.position - manager.transform.position), 0.1f);
                    }
                }
            }
        }

        private void TakeAim()
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, -transform.rotation.y, 0),
                Quaternion.Euler(0, -Mathf.Clamp(_turretRotX * 45, -45, 45), 0), 1f);
        }
    }
}