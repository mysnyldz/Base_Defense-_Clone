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

        private float _turretRot;

        #endregion

        #endregion


        public void TurretRotation(InputParams data)
        {
            if (manager.TurretStates == TurretStates.PlayerOnTurret)
            {
                _turretRot = data.InputValues.x;
                TakeAim();
            }
        }

        private void TakeAim()
        {
            if (_turretRot > 0.15f || _turretRot < -0.15f)
            {
                transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, transform.rotation.y, 0),
                    Quaternion.Euler(0, Mathf.Clamp(_turretRot * 35, -35, 35), 0), 1f);
            }
        }
    }
}