using System;
using Enums;
using Keys;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        #endregion Serialized Variables

        #endregion Self Variables
        
        
        public void ChangeVelocity(InputParams inputParams)
        {
            var VelocityX = inputParams.InputValues.x;
            var VelocityZ = inputParams.InputValues.z;
            playerAnimator.SetFloat("Velocity", (Mathf.Pow(VelocityX, 2) + Mathf.Pow(VelocityZ, 2)) / 2);
        }

        private void ChangeAnimationState(PlayerAnimTypes type, bool mode)
        {
            playerAnimator.SetBool(type.ToString(), mode);
        }

        public void SetPlayerAnimationStateTypes(PlayerStateTypes types)
        {
            ChangeAnimationState(PlayerAnimTypes.IdleMode, false);
            ChangeAnimationState(PlayerAnimTypes.BattleMode, false);
            ChangeAnimationState(PlayerAnimTypes.TargetMode, false);
            ChangeAnimationState(PlayerAnimTypes.TurretMode, false);
            ChangeAnimationState(PlayerAnimTypes.DeathMode, false);
            switch (types)
            {
                case PlayerStateTypes.Idle:
                    ChangeAnimationState(PlayerAnimTypes.IdleMode, true);
                    playerAnimator.SetLayerWeight(1,0);
                    break;
                case PlayerStateTypes.Battle:
                    ChangeAnimationState(PlayerAnimTypes.BattleMode, true);
                    playerAnimator.SetLayerWeight(1,1);
                    break;
                case PlayerStateTypes.Target:
                    ChangeAnimationState(PlayerAnimTypes.TargetMode, true);
                    break;
                case PlayerStateTypes.Turret:
                    ChangeAnimationState(PlayerAnimTypes.TurretMode, true);
                    playerAnimator.SetLayerWeight(1,0);
                    break;
                case PlayerStateTypes.Death:
                    ChangeAnimationState(PlayerAnimTypes.DeathMode,true);
                    break;
            }
        }
    }
}